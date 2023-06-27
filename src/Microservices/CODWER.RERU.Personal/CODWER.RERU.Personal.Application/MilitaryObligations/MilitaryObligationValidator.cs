using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.MilitaryObligations
{
    public class MilitaryObligationValidator : AbstractValidator<MilitaryObligationDto>
    {
       public MilitaryObligationValidator(AppDbContext appDbContext)
       {
              RuleFor(x => (int)x.MilitaryObligationType)
                     .SetValidator(new ExistInEnumValidator<MilitaryObligationTypeEnum>());

              RuleFor(x => x.MilitaryObligationType)
                     .NotEmpty()
                     .WithErrorCode(ValidationCodes.EMPTY_MILITARY_OBLIGATION_TYPE)
                     .WithMessage(ValidationMessages.InvalidInput);

              RuleFor(x => x.ContractorId)
                     .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.EMPTY_BULLETIN_EMITTER,
                     ValidationMessages.InvalidReference));

              When(x => x.ContractorId != null, () =>
              {
                     RuleFor(x => x.ContractorId)
                     .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.EMPTY_BULLETIN_EMITTER,
                     ValidationMessages.InvalidReference));
              });

              When(x => x.StartObligationPeriod != null && x.EndObligationPeriod != null, () =>
              {
                     RuleFor(x => x)
                     .Must(x => x.StartObligationPeriod.Value.Date < x.EndObligationPeriod.Value.Date)
                     .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
              });

              When(x => x.MobilizationYear != null && x.WithdrawalYear != null, () =>
              {
                     RuleFor(x => x)
                     .Must(x => x.MobilizationYear.Value.Date < x.WithdrawalYear.Value.Date)
                     .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
              });
       }
    }
}
