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


            When(x => x.YearOfAdmission != null && x.GraduationYear != null, () =>
            {
                RuleFor(x => x)
                   .Must(x => x.YearOfAdmission < x.GraduationYear)
                    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);
            });
        }
    }
}
