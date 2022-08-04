using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using RERU.Data.Entities.Enums;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Studies
{
    public class StudyValidator:AbstractValidator<StudyDataDto>
    {
        public StudyValidator(AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.StudyFrequency)
               .SetValidator(new ExistInEnumValidator<StudyFrequencyEnum>());

            RuleFor(x => x.StudyTypeId)
                .SetValidator(new ItemMustExistValidator<StudyType>(appDbContext, ValidationCodes.EMPTY_BULLETIN_EMITTER,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.YearOfAdmission)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_STUDY_YEAR_OF_AMISION)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.GraduationYear)
                .NotNull()
                .WithErrorCode(ValidationCodes.EMPTY_STUDY_GRADUATION_YEAR)
                .WithMessage(ValidationMessages.InvalidInput);
        }
    }
}
