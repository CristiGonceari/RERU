using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Organigrams.GetOrganigramUsers
{
    public class GetOrganigramUsersQueryValidator : AbstractValidator<GetOrganigramUsersQuery>
    {
        public GetOrganigramUsersQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.INVALID_DEPARTMENT_ID,
                    ValidationMessages.InvalidReference));

        }
    }
}
