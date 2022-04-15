using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;


namespace CODWER.RERU.Core.Application.Articles.PrintArticles
{
    public class PrintArticlesCommandValidator : AbstractValidator<PrintArticlesCommand>
    {
        public PrintArticlesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x=>x.Value).ToList())
                .SetValidator(new TableExporterValidator<ArticleCoreDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
