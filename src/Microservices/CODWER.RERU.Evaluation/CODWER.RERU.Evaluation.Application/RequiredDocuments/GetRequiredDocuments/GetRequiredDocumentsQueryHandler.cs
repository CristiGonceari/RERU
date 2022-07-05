using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
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
            var items = GetAndFilterRequiredDocuments.Filter(_appDbContext, request.Name, request.Mandatory);

            return await _paginationService.MapAndPaginateModelAsync<RequiredDocument, RequiredDocumentDto>(items, request);
        }
    }
}
