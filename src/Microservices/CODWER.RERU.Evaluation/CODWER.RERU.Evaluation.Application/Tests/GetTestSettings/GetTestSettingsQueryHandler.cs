using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestSettings
{
    public class GetTestSettingsQueryHandler : IRequestHandler<GetTestSettingsQuery, TestDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetTestSettingsQueryHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<TestDto> Handle(GetTestSettingsQuery request, CancellationToken cancellationToken)
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

            return testDto;
        }
    }
}
