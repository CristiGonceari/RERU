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

            eventDiagram.EventsDiagram = _appDbContext.EventVacantPositions
                .Include(x => x.Event)
                .Where(x => x.CandidatePositionId == request.PositionId)
                .OrderBy(x => x.EventId)
                .Select(x => _mapper.Map<EventDiagramDto>(x))
                .ToList();

            foreach (var positionEvent in eventDiagram.EventsDiagram)
            {
                positionEvent.TestTemplates = _appDbContext.EventTestTemplates
                    .Include(x => x.TestTemplate)
                    .Where(x => x.EventId == positionEvent.EventId)
                    .OrderBy(x => x.EventId)
                    .Select(x => _mapper.Map<TestTemplateDiagramDto>(x))
                    .ToList();

                var eventUsers = _appDbContext.EventUsers
                    .Include(x => x.UserProfile)
                    .Where(x => x.EventId == positionEvent.EventId)
                    .OrderBy(x => x.EventId)
                    .Select(x => _mapper.Map<UserDiagramDto>(x))
                    .ToList();

                allUsers.AddRange(eventUsers);

                CalculateTestsFromTestTemplates(eventDiagram, allUsers, positionEvent);
            }

            return eventDiagram;
        }

        private void CalculateTestsFromTestTemplates(PositionDiagramDto eventDiagram, List<UserDiagramDto> allUsers, EventDiagramDto positionEvent)
        {
            eventDiagram.UsersDiagram = allUsers.GroupBy(x => x.UserProfileId).Select(x => x.First()).ToList();

            foreach (var user in eventDiagram.UsersDiagram)
            {
                foreach (var testTemplate in positionEvent.TestTemplates)
                {
                    var testsByTestTemplate = _appDbContext.EventTestTemplates
                        .Where(x => x.EventId == positionEvent.EventId &&
                                    x.TestTemplateId == testTemplate.TestTemplateId)
                        .OrderBy(x => x.EventId)
                        .Select(x => _mapper.Map<TestsByTestTemplateDiagramDto>(x))
                        .ToList();

                    user.TestsByTestTemplate.AddRange(testsByTestTemplate);

                    foreach (var test in testsByTestTemplate)
                    {
                        var tests = _appDbContext.Tests
                            .Where(x => x.UserProfileId == user.UserProfileId &&
                                        x.EventId == positionEvent.EventId &&
                                        x.TestTemplateId == test.TestTemplateId)
                            .OrderBy(x => x.EventId)
                            .Select(x => _mapper.Map<TestResultDiagramDto>(x))
                            .ToList();

                        test.Tests.AddRange(tests);
                    }
                }
            }
        }
    }
}
