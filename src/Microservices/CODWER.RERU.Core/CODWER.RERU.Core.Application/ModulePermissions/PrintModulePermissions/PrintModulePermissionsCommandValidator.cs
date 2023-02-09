using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Core.Application.ModulePermissions.PrintModulePermissions
{
    public class PrintModulePermissionsCommandValidator : AbstractValidator<PrintModulePermissionsCommand>
    {
        public PrintModulePermissionsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<ModulePermissionRowDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
