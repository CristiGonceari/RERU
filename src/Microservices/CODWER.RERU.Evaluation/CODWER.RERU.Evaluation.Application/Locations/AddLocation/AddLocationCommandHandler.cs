using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.AddLocation
{
    public class AddLocationCommandHandler : IRequestHandler<AddLocationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddLocationCommandHandler> _loggerService;

        public AddLocationCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<AddLocationCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddLocationCommand request, CancellationToken cancellationToken)
        {
            var locationToCreate = _mapper.Map<Location>(request.Data);

            await _appDbContext.Locations.AddAsync(locationToCreate);

            await _appDbContext.SaveChangesAsync();

            await LogAction(locationToCreate);

            return locationToCreate.Id;
        }

        private async Task LogAction(Location item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Location was created", item));
        }
    }
}
