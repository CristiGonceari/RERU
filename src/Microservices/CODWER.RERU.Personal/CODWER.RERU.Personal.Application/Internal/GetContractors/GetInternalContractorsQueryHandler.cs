using System.Linq;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Internal.GetContractors
{
    public class GetInternalContractorsQueryHandler : IRequestHandler<GetInternalContractorsQuery, PaginatedModel<ContractorSelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetInternalContractorsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ContractorSelectItem>> Handle(GetInternalContractorsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Contractors
                .Include(c => c.Positions)
                    .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                    .ThenInclude(p => p.Role)
                .AsQueryable();

            var keywordWords = request.Keyword.Split(' ').ToList();

            if (keywordWords.Count == 1)
            {
                items = items.Where(x =>
                    x.FirstName.Contains(keywordWords.First())
                    || x.LastName.Contains(keywordWords.First()));
            }
            else if (keywordWords.Count == 2)
            {
                items = _appDbContext.Contractors.Where(x =>
                    x.FirstName.Contains(keywordWords.First())
                    || x.LastName.Contains(keywordWords.First())
                    || x.FirstName.Contains(keywordWords.Last())
                    || x.LastName.Contains(keywordWords.Last()));
            }
            else
            {
                items = items.Where(x => keywordWords.Contains(x.FirstName) || keywordWords.Contains(x.LastName));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Contractor, ContractorSelectItem>(items, request);

            return paginatedModel;
        }
    }
}
