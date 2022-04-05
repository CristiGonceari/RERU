using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation
{
    public class AssignResponsiblePersonToLocationCommandValidator : AbstractValidator<AssignResponsiblePersonToLocationCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AssignResponsiblePersonToLocationCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r != null, () =>
            {
                RuleFor(r => r.LocationId)
                 .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r)
                   .MustAsync((x, cancellation) => ExistentUser(x))
                    .WithErrorCode(ValidationCodes.INVALID_USER)
                    .WithMessage(ValidationMessages.InvalidReference);

            });
        }
        private async Task<bool> ExistentUser(AssignResponsiblePersonToLocationCommand data)
        {
            var listOfResults = new List<bool>();

            foreach (var userId in data.UserProfileId)
            {
                var result = _appDbContext.UserProfiles.Any(up => up.Id == userId);

                listOfResults.Add(result);

            }

            if (listOfResults.Contains(false))
            {
                return false;
            }

            return true;
        }
    }
}
