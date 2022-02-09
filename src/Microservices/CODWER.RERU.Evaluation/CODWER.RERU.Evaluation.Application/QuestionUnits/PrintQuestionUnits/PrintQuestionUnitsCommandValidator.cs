using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TablePrinterService.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.PrintQuestionUnits
{
    public class PrintQuestionUnitsCommandValidator : AbstractValidator<PrintQuestionUnitsCommand>
    {
        public PrintQuestionUnitsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TablePrinterValidator<QuestionUnitDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
