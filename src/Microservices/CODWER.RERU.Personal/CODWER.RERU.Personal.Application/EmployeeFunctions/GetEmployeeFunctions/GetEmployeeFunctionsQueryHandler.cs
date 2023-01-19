using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.GetEmployeeFunctions
{
    public class GetEmployeeFunctionsQueryHandler : IRequestHandler<GetEmployeeFunctionsQuery, PaginatedModel<EmployeeFunctionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEmployeeFunctionsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<EmployeeFunctionDto>> Handle(GetEmployeeFunctionsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.EmployeeFunctions
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.SearchWord.ToLower()));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<EmployeeFunction, EmployeeFunctionDto>(items, request);

            return paginatedModel;
        }
    }
}
