using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using RERU.Data.Entities.Enums;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities;
using System;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace CODWER.RERU.Personal.Application.Studies
{
    public class StudyValidator:AbstractValidator<StudyDataDto>
    {
        public StudyValidator(AppDbContext appDbContext)
        {
            When(x => x.StudyFrequency != null , () =>
            {
                RuleFor(x => (int)x.StudyFrequency)
                .SetValidator(new ExistInEnumValidator<StudyFrequencyEnum>());
            });

            When(x => x.StudyProfile != null, () =>
            {
                RuleFor(x => (int)x.StudyProfile)
                .SetValidator(new ExistInEnumValidator<StudyProfileEnum>());
            });

            When(x => x.StudyCourse != null, () =>
            {
                RuleFor(x => (int)x.StudyCourse)
                .SetValidator(new ExistInEnumValidator<StudyCourseType>());
            });

            RuleFor(x => x.StudyTypeId)
                .SetValidator(new ItemMustExistValidator<StudyType>(appDbContext, ValidationCodes.EMPTY_BULLETIN_EMITTER,
                    ValidationMessages.InvalidReference));

            When(x => x.StartStudyPeriod != null && x.EndStudyPeriod != null, () =>
            {
                RuleFor(x => x)
                    .Must(x => x.StartStudyPeriod.Value.Date < x.EndStudyPeriod.Value.Date)
                    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
            });

            When(x => x.YearOfAdmission != null && x.GraduationYear != null, () =>
            {
                RuleFor(x => x)
                    .Must(x => Int64.Parse(x.YearOfAdmission) < Int64.Parse(x.GraduationYear))
                    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
            });
        }
    }
}
