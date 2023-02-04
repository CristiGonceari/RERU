using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedModel<ArticleEv360Dto>>
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

        public async Task<PaginatedModel<ArticleEv360Dto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplication.Get();

            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name, currentUser);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ArticleEv360, ArticleEv360Dto>(articles, request);

            return paginatedModel;
        }
    }
}
