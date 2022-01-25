using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.GetPosition
{
    public class GetPositionQueryHandler : IRequestHandler<GetPositionQuery, PositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPositionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PositionDto> Handle(GetPositionQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Positions
                .Include(x=>x.Department)
                .Include(x=>x.OrganizationRole)
                .Include(x => x.Contractor)
                .Include(x => x.Order)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<PositionDto>(item);

            return mappedItem;
        }
    }
}
