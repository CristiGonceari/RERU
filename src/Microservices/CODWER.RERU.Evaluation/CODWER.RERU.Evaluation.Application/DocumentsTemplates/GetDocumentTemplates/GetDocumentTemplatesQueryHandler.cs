using CODWER.RERU.Evaluation.Data.Entities.Documents;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            var items = _appDbContext.DocumentTemplates
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (Enum.IsDefined(typeof(FileTypeEnum), request.fileType))
            {
                items = items.Where(x => x.FileType == request.fileType);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DocumentTemplate, AddEditDocumentTemplateDto>(items, request);

            return paginatedModel;

        }
    }
}
