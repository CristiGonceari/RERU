using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.PrintMySolicitedPositions
{
    public class PrintMySolicitedPositionsCommandValidator : AbstractValidator<PrintMySolicitedPositionsCommand>
    {
        public PrintMySolicitedPositionsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<SolicitedCandidatePositionDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
