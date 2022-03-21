using System.Linq;
using CODWER.RERU.Logging.Application.Validation;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TablePrinterService.Validators;
using FluentValidation;

namespace CODWER.RERU.Logging.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandValidator : AbstractValidator<PrintArticlesCommand>
    {
        public PrintArticlesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x=>x.Value).ToList())
                .SetValidator(new TablePrinterValidator<ArticleDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
