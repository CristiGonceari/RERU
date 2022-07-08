using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.GetRequiredDocuments
{
    public class GetRequiredDocumentsQueryHandler : IRequestHandler<GetRequiredDocumentsQuery, PaginatedModel<RequiredDocumentDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetRequiredDocumentsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<RequiredDocumentDto>> Handle(GetRequiredDocumentsQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.RequiredDocuments
                .AsQueryable()
                .Select(x => new RequiredDocumentDto
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            Mandatory = x.Mandatory
                                        });

            var filteredItems = await Filter(items, request).ToListAsync();

            var paginatedModel = _paginationService.MapAndPaginateModel<RequiredDocumentDto>(filteredItems, request);

            return paginatedModel;
        }

        private IQueryable<RequiredDocumentDto> Filter(IQueryable<RequiredDocumentDto> items, GetRequiredDocumentsQuery request)
        {
            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Mandatory != null)
            {
                items = items.Where(x => x.Mandatory == request.Mandatory);
            }

            return items;
        }
    }
}
