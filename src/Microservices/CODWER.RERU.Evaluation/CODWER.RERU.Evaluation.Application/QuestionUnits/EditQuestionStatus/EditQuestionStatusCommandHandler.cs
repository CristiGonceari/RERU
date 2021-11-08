using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus
{
    public class EditQuestionStatusCommandHandler : IRequestHandler<EditQuestionStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditQuestionStatusCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditQuestionStatusCommand request, CancellationToken cancellationToken)
        {
            var updateQuestionStatus = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == request.Data.QuestionId);

            updateQuestionStatus.Status = request.Data.Status;
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
