using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;
using CODWER.RERU.Logging.Application.Validations;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Logging.Application.PrintLogs
{
    public class PrintLogsCommandValidator : AbstractValidator<PrintLogsCommand>
    {
        public PrintLogsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<LogDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
