using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.Studies;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using System;

namespace CODWER.RERU.Core.Application.Studies
{
    public class StudyValidator : AbstractValidator<StudyDto>
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
