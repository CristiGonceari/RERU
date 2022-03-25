using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.GetDepartments
{
    public class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, PaginatedModel<DepartmentDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetDepartmentsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Departments
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Department, DepartmentDto>(items, request);

            return paginatedModel;
        }
    }
}
