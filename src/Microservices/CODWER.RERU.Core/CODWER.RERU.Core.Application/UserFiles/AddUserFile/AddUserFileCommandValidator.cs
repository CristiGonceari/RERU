using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.UserFiles.AddUserFile
{
    public class AddUserFileCommandValidator : AbstractValidator<AddUserFileCommand>
    {
        public AddUserFileCommandValidator(CoreDbContext coreDbContext) 
        {
            RuleFor(x => x.Data.UserId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(coreDbContext, ValidationCodes.EMPTY_USER_ID,
                    ValidationMessages.NotFound));
        }
    }
}
