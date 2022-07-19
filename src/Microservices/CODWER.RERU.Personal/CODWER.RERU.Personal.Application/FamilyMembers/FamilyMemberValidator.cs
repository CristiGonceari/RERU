using System;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.FamilyMembers
{
    public class FamilyMemberValidator : AbstractValidator<AddEditFamilyMemberDto>
    {
        public FamilyMemberValidator(AppDbContext appDbContext)
        {
            RuleFor(x=>x.FirstName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Birthday)
                .Must(x => x > DateTime.Now.AddYears(-100))
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => BaseNomenclatureTypesEnum.FamilyComponent.NewRecordToValidate(x.RelationId))
                .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));
        }
    }
}
