using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategories
{
    public class GetQuestionCategoriesQueryHandler : IRequestHandler<GetQuestionCategoriesQuery, PaginatedModel<QuestionCategoryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetQuestionCategoriesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<QuestionCategoryDto>> Handle(GetQuestionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = GetAndFilterQuestionCategories.Filter(_appDbContext, request.Name);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<QuestionCategory, QuestionCategoryDto>(categories, request);

            return paginatedModel;
        }
    }
}
