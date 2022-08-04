using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.GetContractorDepartments
{
    public class GetContractorDepartmentsQueryHandler : IRequestHandler<GetContractorDepartmentsQuery, PaginatedModel<ContractorDepartmentDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorDepartmentsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ContractorDepartmentDto>> Handle(GetContractorDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.ContractorDepartments
                .Include(c => c.Contractor)
                .Include(c => c.Department)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }
            if (request.DepartmentId != null)
            {
                items = items.Where(x => x.DepartmentId == request.DepartmentId);
            }
            if (request.FromDateFrom != null)
            {
                items = items.Where(x => x.FromDate > request.FromDateFrom);
            }
            if (request.FromDateTo != null)
            {
                items = items.Where(x => x.FromDate < request.FromDateTo);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ContractorDepartment, ContractorDepartmentDto>(items, request);

            return paginatedModel;
        }
    }
}
