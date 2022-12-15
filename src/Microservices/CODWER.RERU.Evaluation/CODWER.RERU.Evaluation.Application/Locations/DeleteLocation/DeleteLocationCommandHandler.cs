using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.DeleteLocation
{
   public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<DeleteLocationCommandHandler> _loggerService;

        public DeleteLocationCommandHandler(AppDbContext appDbContext, ILoggerService<DeleteLocationCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToDelete = await _appDbContext.Locations.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Locations.Remove(locationToDelete);

            await _appDbContext.SaveChangesAsync();

            await LogAction(locationToDelete);

            return Unit.Value;
        }

        private async Task LogAction(Location item)
        {
            await _loggerService.Log(LogData.AsEvaluation($@"Locația ""{item.Name}"" cu adresa ""{item.Address}"" a fost ștearsă din sistem", item));
        }
    }
}
