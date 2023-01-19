using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.PrintEmployeeFunctions
{
    public class PrintEmployeeFunctionsCommandValidator : AbstractValidator<PrintEmployeeFunctionsCommand>
    {
        public PrintEmployeeFunctionsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<EmployeeFunctionDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
