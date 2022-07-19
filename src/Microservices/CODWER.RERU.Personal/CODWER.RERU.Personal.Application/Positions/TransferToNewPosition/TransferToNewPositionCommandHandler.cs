using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.TransferToNewPosition
{
    public class TransferToNewPositionCommandHandler : IRequestHandler<TransferToNewPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TransferToNewPositionCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(TransferToNewPositionCommand request, CancellationToken cancellationToken)
        {
            var currentPosition = _appDbContext.Positions
                .OrderByDescending(x => x.FromDate)
                .First(x=>x.ContractorId == request.Data.ContractorId);

            currentPosition.ToDate = request.Data.FromDate;

            var newCurrentPosition = _mapper.Map<Position>(request.Data);
            
            await _appDbContext.Positions.AddAsync(newCurrentPosition);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
