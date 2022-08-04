using System;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Ranks
{
    public class RankValidator : AbstractValidator<AddEditRankDto>
    {
        public RankValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Mentions).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x=>x.From)
                .Must(x=>x > DateTime.Now.AddYears(-100))
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => BaseNomenclatureTypesEnum.Rank.NewRecordToValidate(x.RankRecordId))
                .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
