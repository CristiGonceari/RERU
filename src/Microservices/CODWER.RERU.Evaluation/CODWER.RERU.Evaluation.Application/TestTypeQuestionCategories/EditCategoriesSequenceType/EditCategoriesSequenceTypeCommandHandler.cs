using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.EditCategoriesSequenceType
{
    public class EditCategoriesSequenceTypeCommandHandler : IRequestHandler<EditCategoriesSequenceTypeCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public EditCategoriesSequenceTypeCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(EditCategoriesSequenceTypeCommand request, CancellationToken cancellationToken)
        {
            var testType = await _appDbContext.TestTypes.FirstAsync(x => x.Id == request.TestTypeId);

            testType.CategoriesSequence = request.CategoriesSequenceType;
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
