using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.EditCandidate
{
    public class EditCandidateCommandValidator : AbstractValidator<EditCandidateCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public EditCandidateCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            _appDbContext = appDbContext;
            _currentUserProvider = currentUserProvider;


            RuleFor(x => x.Data.Id)
                .SetValidator(x => new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.CandidateCitizenshipId)
                .SetValidator(x => new ItemMustExistValidator<CandidateCitizenship>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

             RuleFor(x => x.Data.CandidateNationalityId)
                .SetValidator(x => new ItemMustExistValidator<CandidateNationality>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            //RuleFor(x => x.Data.WorkPhone).NotEmpty()
            //   .WithMessage(ValidationMessages.InvalidInput)
            //   .WithErrorCode(ValidationCodes.EMPTY_USER_WORK_PHONE);

            //RuleFor(x => x.Data.HomePhone).NotEmpty()
            //   .WithMessage(ValidationMessages.InvalidInput)
            //   .WithErrorCode(ValidationCodes.EMPTY_USER_HOME_PHONE);

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.Data.Id).Result)
               .WithErrorCode(ValidationCodes.USER_NOT_FOUND);

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
