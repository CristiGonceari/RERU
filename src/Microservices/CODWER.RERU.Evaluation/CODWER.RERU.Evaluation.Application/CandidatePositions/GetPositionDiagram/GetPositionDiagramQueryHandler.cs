using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.Extensions;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionDiagram
{
    public class GetPositionDiagramQueryHandler : IRequestHandler<GetPositionDiagramQuery, PositionDiagramDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPositionDiagramQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PositionDiagramDto> Handle(GetPositionDiagramQuery request, CancellationToken cancellationToken)
        {
            var eventDiagram = new PositionDiagramDto();
            var allUsers = new List<UserDiagramDto>();

            eventDiagram.EventsDiagram = GetEvenstDiagram(request.PositionId);

            foreach (var positionEvent in eventDiagram.EventsDiagram)
            {
                positionEvent.TestTemplates = GetTestTemplatesDiagram(positionEvent.EventId);

                if (!positionEvent.TestTemplates.Any())
                {
                    positionEvent.TestTemplates.Add(new TestTemplateDiagramDto());
                }

                var eventUsers = GetUsersDiagram(positionEvent.EventId, request.PositionId);

                allUsers.AddRange(eventUsers);

                eventDiagram.UsersDiagram = allUsers.GroupBy(x => x.UserProfileId).Select(x => x.First()).ToList();

                CalculateTestsFromTestTemplates(eventDiagram, positionEvent, request.PositionId);
            }

            GenerateTestTemplatesForAllUsers(eventDiagram);

            return eventDiagram;
        }

        private List<EventDiagramDto> GetEvenstDiagram(int positionId)
        {
            return _appDbContext.EventVacantPositions
                .Include(x => x.Event)
                .Where(x => x.CandidatePositionId == positionId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<EventDiagramDto>(x))
                .ToList();
        }

        private List<TestTemplateDiagramDto> GetTestTemplatesDiagram(int eventId)
        {
            return _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Where(x => x.EventId == eventId && x.TestTemplate.Status == TestTemplateStatusEnum.Active)
                .OrderBy(x => x.EventId)
                .ThenBy(x => x.TestTemplateId)
                .Select(x => _mapper.Map<TestTemplateDiagramDto>(x))
                .ToList();
        }

        private List<UserDiagramDto> GetUsersDiagram(int eventId, int positionId)
        {
            var users = _appDbContext.EventUserCandidatePositions
                .Include(x => x.EventUser)
                    .ThenInclude(x => x.UserProfile)
                .Where(x => x.EventUser.EventId == eventId && x.CandidatePositionId == positionId)
                .Select(x => x.EventUser.UserProfile)
                .OrderByFullName()
                .ToList();

            var mappedUsers = _mapper.Map<List<UserDiagramDto>>(users);

            return mappedUsers;
        }

        private void CalculateTestsFromTestTemplates(PositionDiagramDto eventDiagram, EventDiagramDto positionEvent, int positionId)
        {
            foreach (var user in eventDiagram.UsersDiagram)
            {
                foreach (var testTemplate in positionEvent.TestTemplates)
                {
                    var testsByTestTemplate = GetTestsByTestTemplateDiagram(positionEvent.EventId, testTemplate.TestTemplateId);

                    user.TestsByTestTemplate.AddRange(testsByTestTemplate);

                    foreach (var test in testsByTestTemplate)
                    {
                        var tests = GetTestsResultDiagram(user.UserProfileId, positionEvent.EventId, test.TestTemplateId, positionId);

                        test.Tests.AddRange(tests);
                    }
                }
            }
        }

        private List<TestsByTestTemplateDiagramDto> GetTestsByTestTemplateDiagram(int eventId, int testTemplateId)
        {
            return _appDbContext.EventTestTemplates
                .Where(x => x.EventId == eventId &&
                            x.TestTemplateId == testTemplateId)
                .OrderBy(x => x.CreateDate)
                .Select(x => _mapper.Map<TestsByTestTemplateDiagramDto>(x))
                .ToList();
        }

        private List<TestResultDiagramDto> GetTestsResultDiagram(int userId,int eventId, int testTemplateId, int positionId)
        {
            var mappedTestList = new List<TestResultDiagramDto>();

            var tests = _appDbContext.Tests
                .Include(x => x.Event)
                .ThenInclude(x => x.EventUsers)
                .ThenInclude(x => x.EventUserCandidatePositions)
                .Where(x => x.UserProfileId == userId &&
                            x.EventId == eventId &&
                            x.TestTemplateId == testTemplateId)
                .OrderBy(x => x.EventId)
                .AsEnumerable()
                .GroupBy(x => x.HashGroupKey)
                .Select(g => g.First())
                .ToList();

            foreach (var test in tests)
            {
                if (test.Event.EventUsers
                    .Where(x => x.UserProfileId == userId)
                    .Any(x => x.EventUserCandidatePositions
                        .Any(x => x.CandidatePositionId == positionId))
                    )
                {
                    var mappedTest = _mapper.Map<TestResultDiagramDto>(test);
                    mappedTestList.Add(mappedTest);
                }
            }

            return mappedTestList;
        }

        private void GenerateTestTemplatesForAllUsers(PositionDiagramDto eventDiagram)
        {
            foreach (var user in eventDiagram.UsersDiagram) 
            {
                foreach (var positionEvent in eventDiagram.EventsDiagram)
                {
                    if (user.TestsByTestTemplate.All(x => x.EventId != positionEvent.EventId))
                    {
                        foreach (var testTemplate in positionEvent.TestTemplates)
                        {
                            user.TestsByTestTemplate.Add(new TestsByTestTemplateDiagramDto()
                            {
                                EventId = positionEvent.EventId,
                                TestTemplateId = testTemplate.TestTemplateId,
                                Tests = new List<TestResultDiagramDto>()
                            });
                        }
                    }
                }

                user.TestsByTestTemplate = user.TestsByTestTemplate.OrderBy(x => x.EventId).ThenBy(x => x.TestTemplateId).ToList();
            }

            eventDiagram.UsersDiagram = eventDiagram.UsersDiagram
                    .OrderByDescending(user => user.TestsByTestTemplate
                        .SelectMany(testTemplate => testTemplate.Tests)
                        .Max(test => test.PassDate))
                    .ToList();

        }
    }
}
