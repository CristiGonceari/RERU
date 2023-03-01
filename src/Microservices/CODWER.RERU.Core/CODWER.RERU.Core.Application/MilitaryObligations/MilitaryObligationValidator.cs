using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.MilitaryObligations
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

            //RuleFor(x => x.MobilizationYear)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.INVALID_INPUT)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.WithdrawalYear)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.INVALID_INPUT)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.Efectiv)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.EMPTY_MILITARY_OBLIGATION_EFECTIVE)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.MilitarySpecialty)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.EMPTY_MILITARY_OBLIGATION_SPECIALITY)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.Degree)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.EMPTY_MILITARY_OBLIGATION_DEGREE)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.MilitaryBookletSeries)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.EMPTY_MILITARY_BOOKLET_SERIES)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.MilitaryBookletNumber)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.EMPTY_MILITARY_BOOKLET_NUMBER)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.MilitaryBookletReleaseDay)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.INVALID_INPUT)
            //       .WithMessage(ValidationMessages.InvalidInput);

            //RuleFor(x => x.MilitaryBookletEminentAuthority)
            //       .NotEmpty()
            //       .WithErrorCode(ValidationCodes.EMPTY_MILITARY_BOOKLET_AUTHORITY)
            //       .WithMessage(ValidationMessages.InvalidInput);
        }
    }
}
