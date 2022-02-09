using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Statistics;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TablePrinterService.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Statistics.PrintCategoryQuestionsStatistic
{
    public class PrintCategoryQuestionsStatisticCommandValidator : AbstractValidator<PrintCategoryQuestionsStatisticCommand>
    {
        public PrintCategoryQuestionsStatisticCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TablePrinterValidator<TestQuestionStatisticDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
