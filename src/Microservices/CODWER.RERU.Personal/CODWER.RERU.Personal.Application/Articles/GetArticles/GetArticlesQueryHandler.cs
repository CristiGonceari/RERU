using CVU.ERP.Common.Pagination;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedModel<ArticleDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetArticlesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfile();

            var articles = GetAndFilterArticles.Filter(_appDbContext, request.Name, currentUser);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Article, ArticleDto>(articles, request);

            return paginatedModel;
        }
    }
}
