using MediatR;
using AutoMapper;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;

namespace CODWER.RERU.Logging.Application.Articles.AddEditArticle
{
    public class AddEditArticleCommandHandler : IRequestHandler<AddEditArticleCommand, int>
    {
        private readonly LoggingDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEditArticleCommandHandler(LoggingDbContext appDbContext, IMapper mapper)
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
