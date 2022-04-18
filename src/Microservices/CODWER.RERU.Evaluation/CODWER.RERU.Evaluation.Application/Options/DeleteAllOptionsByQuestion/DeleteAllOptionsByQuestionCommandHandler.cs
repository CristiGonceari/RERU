using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteAllOptionsByQuestion
{
    public class DeleteAllOptionsByQuestionCommandHandler : IRequestHandler<DeleteAllOptionsByQuestionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteAllOptionsByQuestionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteAllOptionsByQuestionCommand request, CancellationToken cancellationToken)
        {
            var optionsToDelete = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == request.QuestionUnitId)
                .ToListAsync();

            _appDbContext.Options.RemoveRange(optionsToDelete);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
