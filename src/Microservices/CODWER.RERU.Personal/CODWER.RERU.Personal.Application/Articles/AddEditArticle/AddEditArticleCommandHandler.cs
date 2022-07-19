using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Articles.AddEditArticle
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
            var articleToCreate = _mapper.Map<Article>(request.Data);

            if (request.Data.Id.HasValue && _appDbContext.Articles.Any(x => x.Id == request.Data.Id))
            {
                var existingArticle = await _appDbContext.Articles.FirstAsync(x => x.Id == request.Data.Id);
                existingArticle.Name = articleToCreate.Name;
                existingArticle.Content = articleToCreate.Content;
                articleToCreate.Id = existingArticle.Id;
            }
            else
            {
                await _appDbContext.Articles.AddAsync(articleToCreate);
            }

            await _appDbContext.SaveChangesAsync();

            return articleToCreate.Id;
        }
    }
}
