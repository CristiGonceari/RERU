using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.RequiredDocumentPositions.DeleteRequiredDocumentPosition
{
    public class DeleteRequiredDocumentPositionCommandHandler : IRequestHandler<DeleteRequiredDocumentPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteRequiredDocumentPositionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteRequiredDocumentPositionCommand request, CancellationToken cancellationToken)
        {
            var docToDelete = await _appDbContext.RequiredDocumentPositions.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.RequiredDocumentPositions.Remove(docToDelete);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
