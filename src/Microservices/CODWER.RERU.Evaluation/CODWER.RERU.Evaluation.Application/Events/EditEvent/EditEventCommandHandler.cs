using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.EditEvent
{
    public class EditEventCommandHandler : IRequestHandler<EditEventCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditEventCommandHandler> _loggerService;

        public EditEventCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<EditEventCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var editEvent = await _appDbContext.Events.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, editEvent);
            await _appDbContext.SaveChangesAsync();
            await LogAction(editEvent);

            return editEvent.Id;
        }

        private async Task LogAction(Event item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Evenimentul {item.Name} a fost actualizat în sistem", item));
        }
    }
}
