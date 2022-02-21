using System.Linq;
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
            var questionCategory = await _appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            var testTypTesQuestionCategories = _appDbContext.TestTemplateQuestionCategories
                .Include(x=>x.TestCategoryQuestions)
                .Where(x => x.QuestionCategoryId == request.Id);

            _appDbContext.TestTemplateQuestionCategories.RemoveRange(testTypTesQuestionCategories);
            _appDbContext.QuestionCategories.Remove(questionCategory);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
