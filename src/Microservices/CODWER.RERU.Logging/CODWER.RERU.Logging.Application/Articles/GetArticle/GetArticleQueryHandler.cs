using AutoMapper;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Logging.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.Application.Articles.GetArticle
{
    public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleDto>
    {
        private readonly LoggingDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetArticleQueryHandler(LoggingDbContext appDbContext, IMapper mapper)
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
