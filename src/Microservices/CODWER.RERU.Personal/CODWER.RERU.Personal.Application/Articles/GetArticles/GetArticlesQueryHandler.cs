using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CVU.ERP.ServiceProvider;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedModel<ArticleDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ICurrentApplicationUserProvider _currentApplication;

        public GetArticlesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, ICurrentApplicationUserProvider currentApplication)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _currentApplication = currentApplication;
        }

        public async Task<PaginatedModel<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplication.Get();

            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name, currentUser);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Article, ArticleDto>(articles, request);

            return paginatedModel;
        }
    }
}
