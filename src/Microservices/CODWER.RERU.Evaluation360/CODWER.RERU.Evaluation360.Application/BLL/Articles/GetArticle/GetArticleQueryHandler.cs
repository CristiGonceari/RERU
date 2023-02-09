using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticle
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleEv360Dto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetArticleQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ArticleEv360Dto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.Ev360Articles.FirstOrDefaultAsync(x => x.Id == request.Id);

            var mappedItem = _mapper.Map<ArticleEv360Dto>(article);

            mappedItem.Roles = await GetRoles(article.Id);

            return mappedItem;
        }

        private async Task<List<SelectItem>> GetRoles(int articleId)
        {
            return await _appDbContext.ArticleEv360ModuleRoles
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
