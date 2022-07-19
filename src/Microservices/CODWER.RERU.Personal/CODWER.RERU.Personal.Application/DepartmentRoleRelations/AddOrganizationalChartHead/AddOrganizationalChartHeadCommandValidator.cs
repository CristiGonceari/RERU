using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.DepartmentRoleRelations;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddOrganizationalChartHead
{
    public class AddOrganizationalChartHeadCommandValidator : AbstractValidator<AddOrganizationalChartHeadCommand>
    {
        public AddOrganizationalChartHeadCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Type).IsInEnum()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.OrganizationalChartId)
                .SetValidator(new ItemMustExistValidator<OrganizationalChart>(appDbContext, ValidationCodes.ORGANIZATIONAL_CHART_NOT_FOUND, ValidationMessages.InvalidReference));


            RuleFor(x => x.OrganizationalChartId)
                .SetValidator(new ExistentOrganizationalChartHeadValidator(appDbContext, ValidationMessages.InvalidReference));

            When(x => x.Type == OrganizationalChartItemType.Department, () =>
            {
                RuleFor(x => x.HeadId)
                    .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));
            });

            When(x => x.Type == OrganizationalChartItemType.Role, () =>
            {
                RuleFor(x => x.HeadId)
                    .SetValidator(new ItemMustExistValidator<Role>(appDbContext, ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));
            });
        }
    }
}
