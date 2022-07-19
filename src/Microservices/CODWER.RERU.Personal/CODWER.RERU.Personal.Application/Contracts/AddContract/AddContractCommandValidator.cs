﻿using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Contracts.AddContract
{
    public class AddContractCommandValidator : AbstractValidator<AddContractCommand>
    {
        public AddContractCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.Instruction.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            When(x => x.Data.SuperiorId != null, () =>
            {
                RuleFor(x => (int)x.Data.SuperiorId)
                    .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data).Must(x => x.ContractorId != (int)x.SuperiorId);
            });

            RuleFor(x => x.Data).Must(x => x.ContractorId == x.Instruction.ContractorId);

            RuleFor(x => BaseNomenclatureTypesEnum.Currency.NewRecordToValidate(x.Data.CurrencyTypeId))
                .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));
        }
    }
}
