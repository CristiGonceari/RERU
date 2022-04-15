using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Articles.AddEditArticle
{
    public class AddEditArticleCommandHandler : IRequestHandler<AddEditArticleCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEditArticleCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddEditArticleCommand request, CancellationToken cancellationToken)
        {
            var articleToCreate = _mapper.Map<ArticleEvaluation>(request.Data);

            if (request.Data.Id.HasValue && _appDbContext.EvaluationArticles.Any(x => x.Id == request.Data.Id))
            {
                var existingArticle = await _appDbContext.EvaluationArticles.FirstAsync(x => x.Id == request.Data.Id);
                existingArticle.Name = articleToCreate.Name;
                existingArticle.Content = articleToCreate.Content;
                articleToCreate.Id = existingArticle.Id;
            }
            else
            {
                await _appDbContext.EvaluationArticles.AddAsync(articleToCreate);
            }

            await _appDbContext.SaveChangesAsync();

            return articleToCreate.Id;
        }

    }

}
