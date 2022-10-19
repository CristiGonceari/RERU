using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetUserSolicitedPositionDiagram
{
    public class GetUserSolicitedPositionDiagramQueryHandler : IRequestHandler<GetUserSolicitedPositionDiagramQuery, UserPositionDiagramDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetUserSolicitedPositionDiagramQueryHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<UserPositionDiagramDto> Handle(GetUserSolicitedPositionDiagramQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfile();
            var eventDiagram = new UserPositionDiagramDto();

            eventDiagram.EventsDiagram = GetEvenstDiagram(request.PositionId);

            eventDiagram.UserDiagram = request.UserId.HasValue
                ? await GetUserDiagram(request.UserId)
                : _mapper.Map<UserDiagramDto>(currentUser);

            foreach (var positionEvent in eventDiagram.EventsDiagram)
            {
                positionEvent.TestTemplates = GetTestTemplatesDiagram(positionEvent.EventId);

                if (!positionEvent.TestTemplates.Any())
                {
                    positionEvent.TestTemplates.Add(new TestTemplateDiagramDto());
                }

                CalculateTestsFromTestTemplates(eventDiagram.UserDiagram, positionEvent);
            }

            GenerateTestTemplatesForUser(eventDiagram);

            return eventDiagram;
        }

        private List<EventDiagramDto> GetEvenstDiagram(int positionId) => 
             _appDbContext.EventVacantPositions
                .Include(x => x.Event)
                .Where(x => x.CandidatePositionId == positionId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<EventDiagramDto>(x))
                .ToList();

        private List<TestTemplateDiagramDto> GetTestTemplatesDiagram(int eventId) =>
             _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Where(x => x.EventId == eventId)
                .OrderBy(x => x.EventId)
                .ThenBy(x => x.TestTemplateId)
                .Select(x => _mapper.Map<TestTemplateDiagramDto>(x))
                .ToList();

        private async Task<UserDiagramDto> GetUserDiagram(int? userId)
        {
            var user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == userId);

             return _mapper.Map<UserDiagramDto>(user);
        }

        private void CalculateTestsFromTestTemplates(UserDiagramDto user, EventDiagramDto positionEvent)
        {
            foreach (var testTemplate in positionEvent.TestTemplates)
            {
                var testsByTestTemplate = GetTestsByTestTemplateDiagram(positionEvent.EventId, testTemplate.TestTemplateId);

                user.TestsByTestTemplate.AddRange(testsByTestTemplate);

                foreach (var test in testsByTestTemplate)
                {
                    var tests = GetTestsResultDiagram(user.UserProfileId, positionEvent.EventId, test.TestTemplateId);

                    test.Tests.AddRange(tests);
                }
            }
        }

        private List<TestsByTestTemplateDiagramDto> GetTestsByTestTemplateDiagram(int eventId, int testTemplateId)
        {
            return _appDbContext.EventTestTemplates
                .Where(x => x.EventId == eventId &&
                            x.TestTemplateId == testTemplateId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<TestsByTestTemplateDiagramDto>(x))
                .ToList();
        }

        private List<TestResultDiagramDto> GetTestsResultDiagram(int userId, int eventId, int testTemplateId)
        {
            return _appDbContext.Tests
                .Where(x => x.UserProfileId == userId &&
                            x.EventId == eventId &&
                            x.TestTemplateId == testTemplateId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<TestResultDiagramDto>(x))
                .ToList();
        }

        private void GenerateTestTemplatesForUser(UserPositionDiagramDto eventDiagram)
        {
            foreach (var positionEvent in eventDiagram.EventsDiagram)
            {
                if (eventDiagram.UserDiagram.TestsByTestTemplate.All(x => x.EventId != positionEvent.EventId))
                {
                    foreach (var testTemplate in positionEvent.TestTemplates)
                    {
                        eventDiagram.UserDiagram.TestsByTestTemplate.Add(new TestsByTestTemplateDiagramDto()
                        {
                            EventId = positionEvent.EventId,
                            TestTemplateId = testTemplate.TestTemplateId,
                            Tests = new List<TestResultDiagramDto>()
                        });
                    }
                }
            }

            SortTests(eventDiagram.UserDiagram);
        }

        private void SortTests(UserDiagramDto user) =>
            user.TestsByTestTemplate = user.TestsByTestTemplate
                .OrderBy(x => x.EventId)
                .ThenBy(x => x.TestTemplateId)
                .ToList();
    }
}
