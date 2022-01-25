using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.AddPreviousPosition
{
    public class AddPreviousPositionCommandHandler : IRequestHandler<AddPreviousPositionCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddPreviousPositionCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddPreviousPositionCommand request, CancellationToken cancellationToken)
        {
            var mappedPosition = _mapper.Map<Position>(request.Data);

            await _appDbContext.Positions.AddAsync(mappedPosition);
            await _appDbContext.SaveChangesAsync();

            return mappedPosition.Id;
        }
    }
}
