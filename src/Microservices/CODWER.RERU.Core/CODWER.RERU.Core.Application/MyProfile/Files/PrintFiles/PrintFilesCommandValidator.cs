using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Core.Application.MyProfile.Files.PrintFiles
{
    public class PrintFilesCommandValidator : AbstractValidator<PrintFilesCommand>
    {
        public PrintFilesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<GetFilesDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
