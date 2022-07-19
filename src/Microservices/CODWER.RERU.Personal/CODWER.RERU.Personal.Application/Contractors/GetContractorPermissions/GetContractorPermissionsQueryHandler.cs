using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorPermissions
{
    public class GetContractorPermissionsQueryHandler : IRequestHandler<GetContractorPermissionsQuery, ContractorLocalPermissionsDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorPermissionsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ContractorLocalPermissionsDto> Handle(GetContractorPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permission = await _appDbContext.ContractorPermissions
                .FirstOrDefaultAsync(x => x.ContractorId == request.ContractorId);

            if (permission == null)
            {
                return new ContractorLocalPermissionsDto();
            }

            return _mapper.Map<ContractorLocalPermissionsDto>(permission);
        }
    }
}
