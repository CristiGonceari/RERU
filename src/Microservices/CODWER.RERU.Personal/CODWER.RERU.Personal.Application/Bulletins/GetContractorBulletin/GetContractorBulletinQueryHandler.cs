using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Bulletins.GetContractorBulletin
{
    public class GetContractorBulletinQueryHandler : IRequestHandler<GetContractorBulletinQuery, BulletinsDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorBulletinQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<BulletinsDataDto> Handle(GetContractorBulletinQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Bulletins
                .Include(x=>x.Contractor)
                .ThenInclude(x=>x.UserProfile)
                .Include(b => b.BirthPlace)
                .Include(b => b.ResidenceAddress)
                .Include(b => b.ParentsResidenceAddress)
                .FirstAsync(x => x.Contractor.Id == request.ContractorId);

            return _mapper.Map<BulletinsDataDto>(item);
        }
    }
}
