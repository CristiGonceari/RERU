using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserFiles.PrintUserFiles
{
    public class PrintUserFilesValidator : AbstractValidator<PrintUserFilesCommand>
    {
        public PrintUserFilesValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.UserProfileId)
                .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.EMPTY_USER_ID, ValidationMessages.InvalidReference));
        }
    }
}
