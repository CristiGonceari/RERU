using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MaterialStatuses.AddMaterialStatus
{
    public class AddMaterialStatusCommandValidator : AbstractValidator<AddMaterialStatusCommand>
    {
        public AddMaterialStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.MaterialStatusTypeId)
                .SetValidator(new ItemMustExistValidator<MaterialStatusType>(appDbContext, ValidationCodes.MATERIAL_STATUS_TYPE_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }
    }
}
