using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserFiles.AddUserFile
{
    public class AddUserFileCommandValidator : AbstractValidator<AddUserFileCommand>
    {
        public AddUserFileCommandValidator(AppDbContext appDbContext) 
        {
            RuleFor(x => x.Data.UserId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.EMPTY_USER_ID,
                    ValidationMessages.NotFound));
        }
    }
}
