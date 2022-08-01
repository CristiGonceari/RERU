using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.MaterialStatuses.UpdateMaterialStatus
{
    public class UpdateMaterialStatusCommandValidator : AbstractValidator<UpdateMaterialStatusCommand>
    {
        public UpdateMaterialStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.MaterialStatusTypeId)
               .SetValidator(new ItemMustExistValidator<MaterialStatusType>(appDbContext, ValidationCodes.MATERIAL_STATUS_TYPE_NOT_FOUND,
                   ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.Id)
              .SetValidator(new ItemMustExistValidator<MaterialStatus>(appDbContext, ValidationCodes.MATERIAL_STATUS_NOT_FOUND,
                  ValidationMessages.InvalidReference));
        }
    }
}
