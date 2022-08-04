using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using MediatR;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;

namespace CODWER.RERU.Personal.Application.Attestations.AddAttestation
{
    public class AddAttestationCommandHandler : IRequestHandler<AddAttestationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddAttestationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddAttestationCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<Attestation>(request.Data);
            
            await _appDbContext.Attestations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
