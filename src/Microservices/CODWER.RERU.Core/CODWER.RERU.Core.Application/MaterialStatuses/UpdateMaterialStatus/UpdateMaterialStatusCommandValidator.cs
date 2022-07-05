using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MaterialStatuses.UpdateMaterialStatus
{
    public class UpdateMaterialStatusCommandValidator : AbstractValidator<UpdateMaterialStatusCommand>
    {
        public UpdateMaterialStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.MaterialStatusTypeId)
               .SetValidator(new ItemMustExistValidator<MaterialStatusType>(appDbContext, ValidationCodes.MATERIAL_STATUS_TYPE_NOT_FOUND,
                   ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.UserProfileId)
                .SetValidator(new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.Id)
              .SetValidator(new ItemMustExistValidator<MaterialStatus>(appDbContext, ValidationCodes.MATERIAL_STATUS_NOT_FOUND,
                  ValidationMessages.InvalidReference));
        }
    }
}
