using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.UpdateCurrentContractorPosition
{
    public class UpdateCurrentPositionCommandHandler : IRequestHandler<UpdateCurrentPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateCurrentPositionCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCurrentPositionCommand request, CancellationToken cancellationToken)
        {
            var currentPosition = await _appDbContext.Positions
                .OrderByDescending(x=>x.FromDate)
                .FirstAsync(x => x.ContractorId == request.Data.ContractorId);

            _mapper.Map(request.Data, currentPosition);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
