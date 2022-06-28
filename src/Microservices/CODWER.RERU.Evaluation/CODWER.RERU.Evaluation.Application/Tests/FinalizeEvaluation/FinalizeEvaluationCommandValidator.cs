using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeEvaluation
{
    public class FinalizeEvaluationCommandValidator : AbstractValidator<FinalizeEvaluationCommand>
    {
        public FinalizeEvaluationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));
        }
    }
}
