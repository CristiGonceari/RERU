using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.DeleteTestType
{
    public class DeleteTestTypeCommandValidator : AbstractValidator<DeleteTestTypeCommand>
    {
        public DeleteTestTypeCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .Must(x => appDbContext.TestTypes.Any(tt => tt.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);
        }
    }
}
