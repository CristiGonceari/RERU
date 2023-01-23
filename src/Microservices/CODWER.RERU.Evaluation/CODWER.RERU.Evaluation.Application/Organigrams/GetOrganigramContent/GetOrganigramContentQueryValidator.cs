using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Organigrams.GetOrganigramContent
{
    public class GetOrganigramContentQueryValidator : AbstractValidator<GetOrganigramContentQuery>
    {
        public GetOrganigramContentQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.ParentDepartmentId)
                .SetValidator(x => new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.INVALID_DEPARTMENT_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.OrganigramId)
               .SetValidator(x => new ItemMustExistValidator<OrganizationalChart>(appDbContext, ValidationCodes.INVALID_ORGANIGRAM_ID,
                   ValidationMessages.InvalidReference));

        }
    }
}
