using System.Linq;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Roles.PrintRoles
{
    public class PrintRolesCommandValidator : AbstractValidator<PrintRolesCommand>
    {
        public PrintRolesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<RoleDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
