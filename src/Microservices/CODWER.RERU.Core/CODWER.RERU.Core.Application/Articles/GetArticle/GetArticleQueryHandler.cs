using AutoMapper;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Articles.GetArticle
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleDto>
    {
        private readonly CoreDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetArticleQueryHandler(CoreDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ArticleDto> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var article = await _appDbContext.Articles
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<ArticleDto>(article);
        }
    }
}
