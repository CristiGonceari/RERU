using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplates
{
    public class GetTestTemplatesQueryHandler : IRequestHandler<GetTestTemplatesQuery, PaginatedModel<TestTemplateDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetTestTemplatesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<TestTemplateDto>> Handle(GetTestTemplatesQuery request, CancellationToken cancellationToken)
        {
            var testTemplates = GetAndFilterTestTemplates.Filter(_appDbContext, request.Name, request.EventName, request.Status);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Data.Entities.TestTemplate, TestTemplateDto>(testTemplates, request);

            return paginatedModel;
        }
    }
}
