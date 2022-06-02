using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Documents;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.GetDocumentTemplates
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
            var documentTemplates = GetAndFilterDocumentTemplates.Filter(_appDbContext, request.Name, request.FileType);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DocumentTemplate, AddEditDocumentTemplateDto>(documentTemplates, request);

            return paginatedModel;

        }
    }
}
