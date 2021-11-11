using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore
{
    public class AutoCheckTestScoreCommandValidator : AbstractValidator<AutoCheckTestScoreCommand>
    {
        public AutoCheckTestScoreCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x).TestQuestions.Where(x => x.AnswerStatus == AnswerStatusEnum.Answered).All(q => q.IsCorrect.HasValue))
                .WithErrorCode(ValidationCodes.CANT_CALCULATE_RESULT);
        }
    }
}
