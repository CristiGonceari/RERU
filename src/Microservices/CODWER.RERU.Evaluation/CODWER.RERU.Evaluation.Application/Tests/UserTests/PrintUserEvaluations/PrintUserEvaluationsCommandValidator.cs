using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserEvaluations
{
    public class PrintUserEvaluationsCommandValidator : AbstractValidator<PrintUserEvaluationsCommand>
    {
        public PrintUserEvaluationsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<TestDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));

            RuleFor(x => x.UserId)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_USER,
                    ValidationMessages.InvalidReference));
        }
    }
}
