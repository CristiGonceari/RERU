using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.EventUsers.PrintEventUsers
{
    public class PrintEventUsersCommandValidator : AbstractValidator<PrintEventUsersCommand>
    {
        public PrintEventUsersCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<UserProfileDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
