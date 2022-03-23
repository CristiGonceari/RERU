using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.PrintSolicitetTests
{
    public class PrintSolicitetTestsCommandValidator : AbstractValidator<PrintSolicitedTestsCommand>
    {
        public PrintSolicitetTestsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
               .SetValidator(new TableExporterValidator<SolicitedTestDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
