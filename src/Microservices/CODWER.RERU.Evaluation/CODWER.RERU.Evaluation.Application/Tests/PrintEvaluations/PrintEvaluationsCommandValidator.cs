using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.Tests.PrintEvaluations
{
    public class PrintEvaluationsCommandValidator : AbstractValidator<PrintEvaluationsCommand>
    {
        public PrintEvaluationsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<TestDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
