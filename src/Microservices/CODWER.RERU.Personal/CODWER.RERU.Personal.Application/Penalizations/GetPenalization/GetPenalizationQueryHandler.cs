using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Penalizations.GetPenalization
{
    public class GetPenalizationQueryHandler : IRequestHandler<GetPenalizationQuery, PenalizationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPenalizationQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PenalizationDto> Handle(GetPenalizationQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Penalizations
                .Include(x => x.Contractor)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<PenalizationDto>(item);

            return mappedItem;
        }
    }
}
