using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.DeleteTest
{
     public class DeleteTestCommandValidator : AbstractValidator<DeleteTestCommand>
    {
        public DeleteTestCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));
        }
    }
}
