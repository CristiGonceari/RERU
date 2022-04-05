using CODWER.RERU.Logging.Application.Validation;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Logging.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandValidator : AbstractValidator<PrintArticlesCommand>
    {
        public PrintArticlesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x=>x.Value).ToList())
                .SetValidator(new TableExporterValidator<ArticleDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
