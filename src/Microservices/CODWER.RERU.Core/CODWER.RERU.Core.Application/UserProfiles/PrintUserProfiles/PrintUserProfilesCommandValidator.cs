using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfiles
{
    public class PrintUserProfilesCommandValidator : AbstractValidator<PrintUserProfilesCommand>
    {
        public PrintUserProfilesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<UserProfileDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
