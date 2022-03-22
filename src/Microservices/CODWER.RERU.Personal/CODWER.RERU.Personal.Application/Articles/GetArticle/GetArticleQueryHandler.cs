using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

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
            var article = await _appDbContext.Articles
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<ArticleDto>(article);
        }
    }
}
