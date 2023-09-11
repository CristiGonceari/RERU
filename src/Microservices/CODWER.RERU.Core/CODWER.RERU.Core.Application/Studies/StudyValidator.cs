using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.Studies;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Studies
{
    public class StudyValidator : AbstractValidator<StudyDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public StudyValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

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

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.ContractorId).Result)
               .WithErrorCode(ValidationCodes.USER_NOT_FOUND);

            RuleFor(x => x.StudyActRelaseDay)
               .NotEmpty()
               .Must(p => p <= DateTime.Now)
               .WithErrorCode(ValidationCodes.EMPTY_BULLETIN_RELASE_DAY)
               .WithMessage(ValidationMessages.InvalidInput);

        }
        private async Task<bool> CheckIfCurrentUser(int id)
        {
            var currentUser = await _currentUserProvider.Get();
            var contractor = _appDbContext.Contractors.Include(up => up.UserProfile).FirstOrDefault(up => up.UserProfileId.ToString() == currentUser.Id);

            if (contractor.Id == id)
            {
                return true;
            }

            return false;
        }
    }
}
