using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var testTypes = _appDbContext.TestTemplates
                .Include(x => x.TestTypeQuestionCategories)
                .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTypes)
                .ThenInclude(x => x.Event)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                testTypes = testTypes.Where(x => x.Name.Contains(request.Name));
            }

            if (request.Status.HasValue)
            {
                testTypes = testTypes.Where(x => x.Status == request.Status);
            }

            if (!string.IsNullOrEmpty(request.EventName))
            {
                testTypes = testTypes.Where(x => x.EventTestTypes.Any(x => x.Event.Name.Contains(request.EventName)));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Data.Entities.TestTemplate, TestTemplateDto>(testTypes, request);

            return paginatedModel;
        }
    }
}
