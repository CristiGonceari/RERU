﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles.GetArticle
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleEvaluationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetArticleQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ArticleEvaluationDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.EvaluationArticles.FirstOrDefaultAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<ArticleEvaluationDto>(article);

            mappedItem.Roles = await GetRoles(article.Id);

            return mappedItem;
        }

        private async Task<List<SelectItem>> GetRoles(int articleId)
        {
            return await _appDbContext.ArticleEvaluationModuleRoles
                .Include(x => x.Role)
                .Where(x => x.ArticleId == articleId)
                .Select(x => new ModuleRole()
                {
                    Id = x.Role.Id,
                    Name = x.Role.Name
                })
                .Select(e => _mapper.Map<SelectItem>(e))
                .ToListAsync();
        }
    }
}
