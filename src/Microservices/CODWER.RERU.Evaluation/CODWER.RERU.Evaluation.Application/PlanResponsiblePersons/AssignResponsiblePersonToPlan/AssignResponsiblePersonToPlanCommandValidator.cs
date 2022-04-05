using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.AssignResponsiblePersonToPlan
{
    public class AssignResponsiblePersonToPlanCommandValidator : AbstractValidator<AssignResponsiblePersonToPlanCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AssignResponsiblePersonToPlanCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r != null, () =>
            {
                RuleFor(x => x.PlanId)
                    .SetValidator(x => new ItemMustExistValidator<Plan>(appDbContext, ValidationCodes.INVALID_PLAN,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x)
                    .MustAsync((x, cancellation) => ExistentUser(x))
                    .WithErrorCode(ValidationCodes.INVALID_USER)
                    .WithMessage(ValidationMessages.InvalidReference);

            });
        }
        private async Task<bool> ExistentUser(AssignResponsiblePersonToPlanCommand data)
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
