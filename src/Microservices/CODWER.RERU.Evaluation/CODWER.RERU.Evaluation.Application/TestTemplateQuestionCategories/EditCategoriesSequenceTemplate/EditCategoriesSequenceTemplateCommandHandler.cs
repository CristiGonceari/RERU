using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.EditCategoriesSequenceTemplate
{
    public class EditCategoriesSequenceTemplateCommandHandler : IRequestHandler<EditCategoriesSequenceTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditCategoriesSequenceTemplateCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditCategoriesSequenceTemplateCommand request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTemplates.FirstAsync(x => x.Id == request.TestTypeId);

            testType.CategoriesSequence = request.CategoriesSequenceType;
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
