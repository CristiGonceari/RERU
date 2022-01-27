using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.AddUpdateContractorPermissions
{
    public class AddUpdateContractorPermissionsCommandHandler : IRequestHandler<AddUpdateContractorPermissionsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddUpdateContractorPermissionsCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUpdateContractorPermissionsCommand request, CancellationToken cancellationToken)
        {
            var contractor = await _appDbContext.Contractors
                .Include(x => x.Permission)
                .FirstOrDefaultAsync(x=>x.Id == request.Data.ContractorId);

            if (contractor.Permission == null)
            {
                contractor.Permission = _mapper.Map<ContractorLocalPermission>(request.Data);
            }
            else
            {
                _mapper.Map(request.Data, contractor.Permission);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
