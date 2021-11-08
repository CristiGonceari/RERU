using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.UnassignTagFromQuestionUnit
{
    public class UnassignTagFromQuestionUnitCommandHandler : IRequestHandler<UnassignTagFromQuestionUnitCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public UnassignTagFromQuestionUnitCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(UnassignTagFromQuestionUnitCommand request, CancellationToken cancellationToken)
        {
            var questionTag = await _appDbContext.QuestionUnitTags.FirstAsync(x => x.QuestionUnitId == request.QuestionId && x.TagId == request.TagId);

            _appDbContext.QuestionUnitTags.Remove(questionTag);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
