using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfileModules
{
    public class PrintUserProfileModulesCommandValidator : AbstractValidator<PrintUserProfileModulesCommand>
    {
        public PrintUserProfileModulesCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<UserProfileModuleRowDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
