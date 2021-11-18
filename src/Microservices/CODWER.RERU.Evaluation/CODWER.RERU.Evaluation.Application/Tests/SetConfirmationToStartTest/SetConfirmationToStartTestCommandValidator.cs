using FluentValidation;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

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
