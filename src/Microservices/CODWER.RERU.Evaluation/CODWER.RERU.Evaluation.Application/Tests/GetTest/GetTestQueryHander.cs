using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using RERU.Data.Persistence.Context;
using System;
using System.Text;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTest
{
    public class GetTestQueryHander : IRequestHandler<GetTestQuery, TestDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetTestQueryHander(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<TestDto> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUser();

            var test = await _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var testDto = _mapper.Map<TestDto>(test);

            var eventEvaluator = _appDbContext.EventEvaluators.FirstOrDefault(x => x.EvaluatorId == curUser.Id && x.EventId == testDto.EventId);

            if (eventEvaluator != null)
            {
                testDto.ShowUserName = eventEvaluator.ShowUserName;
            }
            else
            {
                testDto.ShowUserName = true;
            }

            if (!string.IsNullOrEmpty(testDto.Rules))
            {
                var base64EncodedBytes = Convert.FromBase64String(testDto.Rules);
                testDto.Rules = Encoding.UTF8.GetString(base64EncodedBytes);
            }

            return testDto;
        }
    }
}
