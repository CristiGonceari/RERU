using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTest
{
    public class GetTestQueryHandler : IRequestHandler<GetTestQuery, TestDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetTestQueryHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<TestDto> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var test = await _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(x => x.TestTemplateModuleRoles)
                .Include(t => t.UserProfile)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var testDto = _mapper.Map<TestDto>(test);

            var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == currentUserId && x.EventId == testDto.EventId);

            if (!string.IsNullOrEmpty(testDto.Rules))
            {
                var base64EncodedBytes = Convert.FromBase64String(testDto.Rules);
                testDto.Rules = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            testDto = await CheckIfHasCandidatePosition(testDto);

            return testDto;
        }

        private async Task<TestDto> CheckIfHasCandidatePosition(TestDto test)
        {
                var eventUser = _appDbContext.EventUsers.FirstOrDefault(x => x.EventId == test.EventId && x.UserProfileId == test.UserId);

                if (eventUser == null) return test;
                {
                    var eventUserCandidatePositions =
                        _appDbContext.EventUserCandidatePositions.Where(x => x.EventUserId == eventUser.Id)
                            .Select(x => x.CandidatePositionId)
                            .ToList();

                    if ((!(eventUser?.PositionId > 0))) return test;
                    {
                        var candidatePositionNames =
                            _appDbContext.CandidatePositions.Where(p => !eventUserCandidatePositions.All(p2 => p2 != p.Id))
                                .Select(x => x.Name)
                                .ToList();

                        test.CandidatePositionNames = candidatePositionNames;
                    }
                }

                return test;
        }
    }
}
