using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contracts.UpdateContract
{
    public class UpdateContractCommandValidator : AbstractValidator<UpdateContractCommand>
    {
        public UpdateContractCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<IndividualContract>(appDbContext, ValidationCodes.CONTRACT_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            When(x => x.Data.SuperiorId != null, () =>
            {
                RuleFor(x => (int)x.Data.SuperiorId)
                    .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data).Must(x => x.ContractorId != x.SuperiorId);
            });

            RuleFor(x => BaseNomenclatureTypesEnum.Currency.NewRecordToValidate(x.Data.CurrencyTypeId))
                .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));
        }
    }
}
