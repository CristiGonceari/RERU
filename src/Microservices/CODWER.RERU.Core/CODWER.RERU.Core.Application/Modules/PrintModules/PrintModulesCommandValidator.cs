using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Core.Application.Modules.PrintModules
{
    public class PrintModulesCommandValidator : AbstractValidator<PrintModulesCommand>
    {
        public PrintModulesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<global::RERU.Data.Entities.Module>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
