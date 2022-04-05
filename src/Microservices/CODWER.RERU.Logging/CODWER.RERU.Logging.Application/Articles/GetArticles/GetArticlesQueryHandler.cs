using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedModel<ArticleDto>>
    {
        private readonly LoggingDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetArticlesQueryHandler(LoggingDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Article, ArticleDto>(articles, request);

            return paginatedModel;
        }
    }
}
