using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.PrintDocumentTemplates
{
    public class PrintDocumentTemplatesCommandValidator : AbstractValidator<PrintDocumentTemplatesCommand>
    {
        public PrintDocumentTemplatesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<AddEditDocumentTemplateDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
