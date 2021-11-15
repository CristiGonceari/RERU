﻿using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Articles.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedModel<ArticleDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetArticlesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var articles = _appDbContext.Articles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                articles = articles.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%"));
            }

            var paginatedModel = _paginationService.MapAndPaginateModel<Article, ArticleDto>(articles, request);

            return paginatedModel;
        }
    }
}
