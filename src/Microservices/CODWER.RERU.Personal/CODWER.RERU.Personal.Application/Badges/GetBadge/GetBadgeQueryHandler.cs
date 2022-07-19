using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Badges.GetBadge
{
    public class GetBadgeQueryHandler : IRequestHandler<GetBadgeQuery, BadgeDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetBadgeQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<BadgeDto> Handle(GetBadgeQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Badges
                .Include(x => x.Contractor)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<BadgeDto>(item);

            return mappedItem;
        }
    }
}
