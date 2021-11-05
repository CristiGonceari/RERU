using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.DeleteQuestionCategory
{
    public class DeleteQuestionCategoryCommandHandler : IRequestHandler<DeleteQuestionCategoryCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteQuestionCategoryCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Unit> Handle(DeleteQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questionCategoryToEdit = await _appDbContext.QuestionCategories
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.QuestionCategories.Remove(questionCategoryToEdit);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
