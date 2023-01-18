using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.PrintOrganizationRoles
{
    public class PrintOrganizationRolesCommandValidator : AbstractValidator<PrintOrganizationRolesCommand>
    {
        public PrintOrganizationRolesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<RoleDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
