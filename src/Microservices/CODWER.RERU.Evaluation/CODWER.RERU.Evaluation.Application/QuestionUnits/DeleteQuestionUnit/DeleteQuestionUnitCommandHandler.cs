using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit
{
    public class DeleteQuestionUnitCommandHandler : IRequestHandler<DeleteQuestionUnitCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteQuestionUnitCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteQuestionUnitCommand request, CancellationToken cancellationToken)
        {
            var questionUnit = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.QuestionUnits.Remove(questionUnit);

            var options = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == request.Id)
                .ToListAsync();

            _appDbContext.Options.RemoveRange(options);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
