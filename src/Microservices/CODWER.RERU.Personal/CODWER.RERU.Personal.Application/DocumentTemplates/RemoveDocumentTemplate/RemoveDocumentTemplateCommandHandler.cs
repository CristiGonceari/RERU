using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.RemoveDocumentTemplate
{
    public class RemoveDocumentTemplateCommandHandler : IRequestHandler<RemoveDocumentTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveDocumentTemplateCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.DocumentTemplates.FirstAsync(d => d.Id == request.Id);

            _appDbContext.DocumentTemplates.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
