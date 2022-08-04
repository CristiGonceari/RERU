using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Attestations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Attestations.GetAttestation
{
    public class GetAttestationQueryHandler : IRequestHandler<GetAttestationQuery, AttestationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAttestationQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<AttestationDto> Handle(GetAttestationQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Attestations
                .Include(x => x.Contractor)
                .FirstAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<AttestationDto>(item);

            return mappedItem;
        }
    }
}
