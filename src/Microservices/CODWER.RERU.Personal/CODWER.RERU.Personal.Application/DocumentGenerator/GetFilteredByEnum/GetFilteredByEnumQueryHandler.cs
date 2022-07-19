using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Documents;
using RERU.Data.Entities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentGenerator.GetFilteredByEnum
{
    public class GetFilteredByEnumQueryHandler: IRequestHandler<GetFilteredByEnumQuery, PaginatedModel<DocumentTemplateGeneratorDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetFilteredByEnumQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<DocumentTemplateGeneratorDto>> Handle(GetFilteredByEnumQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.DocumentTemplates.AsQueryable();

            if (request.File != null)
            {
                items = items.Where(x => x.FileType == request.File);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<DocumentTemplate, DocumentTemplateGeneratorDto>(items, request);

            return paginatedModel;
        }
    }
}
