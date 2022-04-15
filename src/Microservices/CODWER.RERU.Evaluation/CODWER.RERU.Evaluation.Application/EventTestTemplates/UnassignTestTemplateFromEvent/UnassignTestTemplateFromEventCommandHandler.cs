using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.UnassignTestTemplateFromEvent
{
    public class UnassignTestTemplateFromEventCommandHandler : IRequestHandler<UnassignTestTemplateFromEventCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignTestTemplateFromEventCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignTestTemplateFromEventCommand request, CancellationToken cancellationToken)
        {
            var eventTestTemplate = await _appDbContext.EventTestTemplates.FirstAsync(x => x.TestTemplateId == request.TestTemplateId && x.EventId == request.EventId);

            _appDbContext.EventTestTemplates.Remove(eventTestTemplate);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
