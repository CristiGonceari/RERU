using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.EditQuestionCategory
{
    public class EditQuestionCategoryCommandHandler : IRequestHandler<EditQuestionCategoryCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditQuestionCategoryCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questionCategoryToEdit = await _appDbContext.QuestionCategories
                .FirstOrDefaultAsync(x => x.Id == request.Data.Id.Value);

            questionCategoryToEdit.Name = request.Data.Name;

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
