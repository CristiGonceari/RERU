using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.AssignTestTemplateToEvent
{
    public class AssignTestTemplateToEventCommandHandler : IRequestHandler<AssignTestTemplateToEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AssignTestTemplateToEventCommand> _loggerService;

        public AssignTestTemplateToEventCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ILoggerService<AssignTestTemplateToEventCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(AssignTestTemplateToEventCommand request, CancellationToken cancellationToken)
        {
            var eventTestTemplate = _mapper.Map<EventTestTemplate>(request.Data);

            await _appDbContext.EventTestTemplates.AddAsync(eventTestTemplate);
            await _appDbContext.SaveChangesAsync();

            await LogAction(eventTestTemplate.Id);

            return Unit.Value;
        }

        private async Task LogAction(int eventTestTemplateId)
        {
            var eventTestTemplate =  await _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == eventTestTemplateId);

            await _loggerService.Log(LogData.AsEvaluation( $"Șablonul de test {eventTestTemplate.TestTemplate.Name} a fost atașat la evenimentul {eventTestTemplate.Event.Name}"));
        }
    }
}
