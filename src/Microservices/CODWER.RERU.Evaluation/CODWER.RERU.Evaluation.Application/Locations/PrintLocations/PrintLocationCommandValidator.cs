using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TablePrinterService.Validators;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.Locations.PrintLocations
{
    public class PrintLocationCommandValidator : AbstractValidator<PrintLocationCommand>
    {
        public PrintLocationCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TablePrinterValidator<LocationDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
