using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.AddMySolicitedTest
{
    public class AddMySolicitedTestCommandValidator : AbstractValidator<AddMySolicitedTestCommand>
    {
        public AddMySolicitedTestCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.CandidatePositionId)
                .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));
        }
    }
}
