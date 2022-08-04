using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Contractors.SetContractorAvatar
{
    public class SetContractorAvatarCommandValidator : AbstractValidator<SetContractorAvatarCommand>
    {
        public SetContractorAvatarCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }

        private void ValidateExtension(string name, string errorMessage, CustomContext context)
        {
            var splitArray = name.Split('.');

            if (splitArray.Length < 2)
            {
                context.AddFail(ValidationCodes.INVALID_FILE_NAME, errorMessage);
            }

            var extension = splitArray.Last().ToLower();
            if (!FileExtensions().Contains(extension))
            {
                context.AddFail(ValidationCodes.INVALID_FILE_EXTENSION, errorMessage);
            }
        }

        private List<string> FileExtensions() => new() { "jpg", "png", "jpeg" };
    }
}
