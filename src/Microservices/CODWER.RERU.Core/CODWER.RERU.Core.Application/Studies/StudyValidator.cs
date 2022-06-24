using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.Studies;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Studies
{
    public class StudyValidator : AbstractValidator<StudyDto>
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
