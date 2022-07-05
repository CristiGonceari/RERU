using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocuments.DeleteRequiredDocument
{
    public class DeleteRequiredDocumentsCommandHandler : IRequestHandler<DeleteRequiredDocumentsCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteRequiredDocumentsCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteRequiredDocumentsCommand request, CancellationToken cancellationToken)
        {
            var docToDelete = await _appDbContext.RequiredDocuments.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.RequiredDocuments.Remove(docToDelete);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
