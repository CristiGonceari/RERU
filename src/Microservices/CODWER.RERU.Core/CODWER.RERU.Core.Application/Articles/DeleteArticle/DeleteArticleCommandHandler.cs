using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Core.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Articles.DeleteArticle
{
    public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Unit>
    {
        private readonly CoreDbContext _appDbContext;

        public DeleteArticleCommandHandler(CoreDbContext appDbContext)
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
