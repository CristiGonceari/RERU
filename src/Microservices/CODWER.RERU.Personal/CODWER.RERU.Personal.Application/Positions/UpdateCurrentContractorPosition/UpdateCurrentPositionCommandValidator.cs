using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Positions.UpdateCurrentContractorPosition
{
    public class UpdateCurrentPositionCommandValidator : AbstractValidator<UpdateCurrentPositionCommand>
    {
        public UpdateCurrentPositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));

            When(x => x.Data.DepartmentId != null, () =>
            {
                RuleFor(x => (int)x.Data.DepartmentId)
                    .SetValidator(new ItemMustExistValidator<Department>(appDbContext,ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));
            });

            When(x => x.Data.OrganizationRoleId != null, () =>
            {
                RuleFor(x => (int)x.Data.OrganizationRoleId)
                    .SetValidator(new ItemMustExistValidator<OrganizationRole>(appDbContext,ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));
            });
        }
    }
}
