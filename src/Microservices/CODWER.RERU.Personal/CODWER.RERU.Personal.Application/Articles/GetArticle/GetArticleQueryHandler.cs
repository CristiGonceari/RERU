using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Articles.GetArticle
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetArticleQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ArticleDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.Articles.FirstOrDefaultAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<ArticleDto>(article);

            mappedItem.Roles = await GetRoles(article.Id);

            return mappedItem;
        }

        private async Task<List<SelectItem>> GetRoles(int articleId)
        {
            return await _appDbContext.ArticlePersonalModuleRoles
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
