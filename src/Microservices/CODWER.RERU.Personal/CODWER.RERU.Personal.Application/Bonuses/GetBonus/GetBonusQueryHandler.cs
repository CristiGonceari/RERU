using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Bonuses.GetBonus
{
    public class GetBonusQueryHandler : IRequestHandler<GetBonusQuery, BonusDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetBonusQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<BonusDto> Handle(GetBonusQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Bonuses
                .Include(x => x.Contractor)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<BonusDto>(item);

            return mappedItem;
        }
    }
}
