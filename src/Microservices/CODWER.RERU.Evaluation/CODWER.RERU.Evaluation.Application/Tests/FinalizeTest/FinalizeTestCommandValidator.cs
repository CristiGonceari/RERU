using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeTest
{
    public class FinalizeTestCommandValidator : AbstractValidator<FinalizeTestCommand>
    {
        public FinalizeTestCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Must(x => appDbContext.Tests.FirstOrDefault(t => t.Id == x).TestStatus == TestStatusEnum.InProgress &&
                            appDbContext.Tests.Include(t => t.TestTemplate).FirstOrDefault(t => t.Id == x).TestTemplate.Mode == TestTemplateModeEnum.Test)
                .WithErrorCode(ValidationCodes.ONLY_IN_PROGRESS_TESTS_CAN_BE_TERMINATED);
        }
    }
}
