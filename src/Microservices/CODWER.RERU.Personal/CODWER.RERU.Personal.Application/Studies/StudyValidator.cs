using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.Application.Studies
{
    public class StudyValidator:AbstractValidator<StudyDataDto>
    {
        public StudyValidator(AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.StudyFrequency)
                .SetValidator(new ExistInEnumValidator<StudyFrequencyEnum>());

            RuleFor(x => BaseNomenclatureTypesEnum.StudyType.NewRecordToValidate(x.StudyTypeId))
                .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));

            When(x=> !x.IsActiveStudy, () =>
            {
                RuleFor(x => x.DiplomaNumber)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_STUDY_DIPLOMA_NUMBER)
                    .WithMessage(ValidationMessages.InvalidInput);

                RuleFor(x => x.DiplomaReleaseDay)
                    .NotNull()
                    .WithErrorCode(ValidationCodes.EMPTY_STUDY_DIPLOMA_RELEASE_DAY)
                    .WithMessage(ValidationMessages.InvalidInput);
            });
        }
    }
}
