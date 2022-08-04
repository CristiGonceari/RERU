using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.MaterialStatus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.MaterialStatuses.GetContractorMaterialStatus
{
    public class GetContractorMaterialStatusQueryHandler : IRequestHandler<GetContractorMaterialStatusQuery, AddEditMaterialStatusDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorMaterialStatusQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<AddEditMaterialStatusDto> Handle(GetContractorMaterialStatusQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.MaterialStatuses
                                            .Include(ms => ms.MaterialStatusType)
                                            .FirstOrDefaultAsync(ms => ms.ContractorId == request.ContractorId);

            return _mapper.Map<AddEditMaterialStatusDto>(item);
        }
    }
}
