using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.EditLocation
{
    public class EditLocationCommandHandler : IRequestHandler<EditLocationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditLocationCommandHandler> _loggerService;

        public EditLocationCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<EditLocationCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<int> Handle(EditLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToEdit = await _appDbContext.Locations.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, locationToEdit);
            await _appDbContext.SaveChangesAsync();

            await LogAction(locationToEdit);

            return locationToEdit.Id;
        }

        private async Task LogAction(Location item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Location was edited", item));
        }
    }
}
