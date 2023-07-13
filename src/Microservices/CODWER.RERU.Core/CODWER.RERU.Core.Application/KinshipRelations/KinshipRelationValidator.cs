using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelations
{
    public class KinshipRelationValidator : AbstractValidator<KinshipRelationDto>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly AppDbContext _appDbContext;

        public KinshipRelationValidator(AppDbContext appDbContext, IDateTime dateTime, ICurrentApplicationUserProvider currentUserProvider)
        {
            _currentUserProvider = currentUserProvider;
            _appDbContext = appDbContext;

            RuleFor(x => (int)x.KinshipDegree)
               .SetValidator(new ExistInEnumValidator<KinshipDegreeEnum>());

            RuleFor(x => x.Name)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.LastName)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_LAST_NAME)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.BirthDate)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.INVALID_INPUT)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.BirthLocation)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_BIRTH_LOCATION)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Function)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_FUNCTION)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.WorkLocation)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_WORK_LOCATION)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.ResidenceAddress)
                   .NotNull()
                   .WithErrorCode(ValidationCodes.EMPTY_KINSHIP_RELATION_RESIDENCE_ADDRESS)
                   .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(r => r.BirthDate)
               .Must(x => x < dateTime.Now.AddYears(-18))
               .WithErrorCode(ValidationCodes.INVALID_USER_BIRTH_DATE);

            RuleFor(x => x)
               .Must(x => CheckIfCurrentUser(x.ContractorId).Result)
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
