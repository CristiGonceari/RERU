using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.AddEvent
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddEventCommandHandler> _loggerService;

        public AddEventCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<AddEventCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            var eventToCreate = _mapper.Map<Event>(request.Data);

            await _appDbContext.Events.AddAsync(eventToCreate);
            await _appDbContext.SaveChangesAsync();
            await LogAction(eventToCreate);

            return eventToCreate.Id;
        }

        private async Task LogAction(Event item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Event was created", item));
        }
    }
}
