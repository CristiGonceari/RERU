using System;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.Application.Contractors
{
    public class ContractorValidator : AbstractValidator<AddEditContractorDto>
    {
        public ContractorValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.FatherName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.BirthDate)
                .Must(x => x > DateTime.Now.AddYears(-100))
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => (int)x.Sex)
                .SetValidator(new ExistInEnumValidator<SexTypeEnum>());

            RuleFor(x => BaseNomenclatureTypesEnum.BloodTypes.NewRecordToValidate(x.BloodTypeId))
                .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));
        }
    }
}
