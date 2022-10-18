using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteArticleCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
        {
            var articleToDelete = await _appDbContext.Articles.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Articles.Remove(articleToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
