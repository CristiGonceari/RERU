using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Articles.RemoveArticle
{
    public class RemoveArticleCommandHandler : IRequestHandler<RemoveArticleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveArticleCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken)
        {
            var articleToDelete = await _appDbContext.Articles.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Articles.Remove(articleToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
