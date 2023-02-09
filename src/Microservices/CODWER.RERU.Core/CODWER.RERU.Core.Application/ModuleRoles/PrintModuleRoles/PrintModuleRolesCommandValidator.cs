using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Core.Application.ModuleRoles.PrintModuleRoles
{
    public class PrintModuleRolesCommandValidator : AbstractValidator<PrintModuleRolesCommand>
    {
        public PrintModuleRolesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<ModuleRoleRowDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
