using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Documents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.GetDocumentTemplates
{
    public class GetDocumentTemplatesQueryHandler : IRequestHandler<GetDocumentTemplatesQuery, PaginatedModel<AddEditDocumentTemplateDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetDocumentTemplatesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<AddEditDocumentTemplateDto>> Handle(GetDocumentTemplatesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.DocumentTemplates
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DocumentTemplate, AddEditDocumentTemplateDto>(items, request);

            return paginatedModel;

        }
    }
}
