using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedModel<ArticleEvaluationDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetArticlesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ArticleEvaluationDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ArticleEvaluation, ArticleEvaluationDto>(articles, request);

            return paginatedModel;
        }
    }
}
