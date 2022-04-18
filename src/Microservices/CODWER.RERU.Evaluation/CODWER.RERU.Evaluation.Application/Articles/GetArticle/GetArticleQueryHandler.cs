using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
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
            var article = await _appDbContext.EvaluationArticles
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<ArticleEvaluationDto>(article);
        }
    }
}
