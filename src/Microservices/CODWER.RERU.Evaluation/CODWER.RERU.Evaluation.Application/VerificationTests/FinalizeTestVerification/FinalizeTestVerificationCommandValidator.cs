using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.FinalizeTestVerification
{
    public class FinalizeTestVerificationCommandValidator : AbstractValidator<FinalizeTestVerificationCommand>
    {
        public FinalizeTestVerificationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Must(x => appDbContext.Tests.First(t => t.Id == x).TestStatus == TestStatusEnum.Terminated)
                .WithErrorCode(ValidationCodes.ONLY_TERMINATED_TEST_CAN_BE_SET_VERIFIED);

            RuleFor(x => x.TestId)
                .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x).TestQuestions.Where(v => v.AnswerStatus == AnswerStatusEnum.Answered)
                    .All(q => q.Verified == VerificationStatusEnum.Verified || q.Verified == VerificationStatusEnum.VerifiedBySystem))
                .WithErrorCode(ValidationCodes.NOT_ALL_ANSWERS_ARE_VERIFIED);
        }
    }
}
