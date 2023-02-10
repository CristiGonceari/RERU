using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.PrintMyEvaluations
{
    public class PrintMyEvaluationsCommandValidator : AbstractValidator<PrintMyEvaluationsCommand>
    {
        public PrintMyEvaluationsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<TestDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
