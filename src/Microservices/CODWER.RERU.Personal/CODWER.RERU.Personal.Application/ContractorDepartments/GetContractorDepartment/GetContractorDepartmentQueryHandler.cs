using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartment
{
    public class GetContractorDepartmentQueryHandler : IRequestHandler<GetContractorDepartmentQuery, ContractorDepartmentDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorDepartmentQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ContractorDepartmentDto> Handle(GetContractorDepartmentQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.ContractorDepartments
                .Include(c => c.Contractor)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<ContractorDepartmentDto>(item);

            return mappedItem;
        }
    }
}
