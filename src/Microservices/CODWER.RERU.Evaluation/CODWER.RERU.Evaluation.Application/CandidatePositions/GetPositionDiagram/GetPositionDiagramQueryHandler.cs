using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.PositionDiagram;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

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
            var allTestTemplates = new List<TestTemplateDiagramDto>();

            eventDiagram.EventsDiagram = GetEvenstDiagram(request.PositionId);

            foreach (var positionEvent in eventDiagram.EventsDiagram)
            {
                positionEvent.TestTemplates = GetTestTemplatesDiagram(positionEvent.EventId);

                var eventUsers = GetUsersDiagram(positionEvent.EventId);

                allUsers.AddRange(eventUsers);
                allTestTemplates.AddRange(positionEvent.TestTemplates);

                eventDiagram.UsersDiagram = allUsers.GroupBy(x => x.UserProfileId).Select(x => x.First()).ToList();

                CalculateTestsFromTestTemplates(eventDiagram, positionEvent);
            }

            GenerateTestTemplatesForAllUsers(eventDiagram, allTestTemplates);

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
                .Where(x => x.EventId == eventId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<TestTemplateDiagramDto>(x))
                .ToList();
        }

        private List<UserDiagramDto> GetUsersDiagram(int eventId)
        {
            return _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == eventId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<UserDiagramDto>(x))
                .ToList();
        }

        private void CalculateTestsFromTestTemplates(PositionDiagramDto eventDiagram, EventDiagramDto positionEvent)
        {
            foreach (var user in eventDiagram.UsersDiagram)
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

        private List<TestResultDiagramDto> GetTestsResultDiagram(int userId,int eventId, int testTemplateId)
        {
            return _appDbContext.Tests
                .Where(x => x.UserProfileId == userId &&
                            x.EventId == eventId &&
                            x.TestTemplateId == testTemplateId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<TestResultDiagramDto>(x))
                .ToList();
        }

        private void GenerateTestTemplatesForAllUsers(PositionDiagramDto eventDiagram, List<TestTemplateDiagramDto> allTestTemplates)
        {
            foreach (var positionEvent in eventDiagram.EventsDiagram)
            {
                foreach (var user in eventDiagram.UsersDiagram)
                {
                    foreach (var testTemplate in positionEvent.TestTemplates)
                    {
                        if (user.TestsByTestTemplate.Count() < allTestTemplates.Count())
                        {
                            if (user.TestsByTestTemplate.Any(x => x.TestTemplateId != testTemplate.TestTemplateId))
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
            }
        }
    }
}
