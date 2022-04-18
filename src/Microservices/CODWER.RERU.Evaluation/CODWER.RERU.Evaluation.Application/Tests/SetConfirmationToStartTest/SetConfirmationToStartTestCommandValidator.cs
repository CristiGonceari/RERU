using FluentValidation;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest
{
    public class SetConfirmationToStartTestCommandValidator : AbstractValidator<SetConfirmationToStartTestCommand>
    {
        private readonly IUserProfileService _userService;

        public SetConfirmationToStartTestCommandValidator(AppDbContext appDbContext, IUserProfileService userService)
        {
            _userService = userService;

            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));
        }
       
    }
}
