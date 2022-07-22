using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.PrintSolicitedPositions
{
    public class PrintSolicitedPositionsCommandValidator : AbstractValidator<PrintSolicitedPositionsCommand>
    {
        public PrintSolicitedPositionsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
               .SetValidator(new TableExporterValidator<SolicitedCandidatePositionDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
