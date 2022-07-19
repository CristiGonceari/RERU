using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
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

            When(x => x.Data.RoleId != null, () =>
            {
                RuleFor(x => (int)x.Data.RoleId)
                    .SetValidator(new ItemMustExistValidator<Role>(appDbContext,ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));
            });
        }
    }
}
