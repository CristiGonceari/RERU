using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetEventTestTemplates
{
    public class GetEventTestTemplatesQueryHandler : IRequestHandler<GetEventTestTemplatesQuery, PaginatedModel<TestTemplateDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventTestTemplatesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<TestTemplateDto>> Handle(GetEventTestTemplatesQuery request, CancellationToken cancellationToken)
        {
            var eventTestTemplates = _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.TestTemplate)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<TestTemplate, TestTemplateDto>(eventTestTemplates, request);
        }
    }
}
