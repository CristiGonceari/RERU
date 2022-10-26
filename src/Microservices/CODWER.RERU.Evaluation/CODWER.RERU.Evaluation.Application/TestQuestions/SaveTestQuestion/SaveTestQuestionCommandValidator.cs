using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.Validators.TestValidators;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.SaveTestQuestion
{
    public class SaveTestQuestionCommandValidator : AbstractValidator<SaveTestQuestionCommand>
    {
        public SaveTestQuestionCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            RuleFor(x => x.Data.Status)
                .NotNull()
                .IsInEnum()
                .WithErrorCode(ValidationCodes.INVALID_ANSWER_STATUS);

            When(x => x.Data.QuestionUnitId.HasValue, () =>
            {
                RuleFor(x => x.Data)
                    .Must(x => appDbContext.TestQuestions
                        .Any(t => t.Id == appDbContext.Tests
                            .FirstOrDefault(ts => ts.Id == x.TestId)
                            .TestQuestions
                            .FirstOrDefault(q => q.QuestionUnitId == x.QuestionUnitId).Id))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);
            });

            When(x => x.Data.QuestionIndex.HasValue, () => 
            {
                RuleFor(x => x.Data)
                    .Must(x => appDbContext.TestQuestions
                        .Any(t => t.Id == appDbContext.Tests
                            .FirstOrDefault(ts => ts.Id == x.TestId)
                            .TestQuestions
                            .FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

                When(x => !appDbContext.Tests.Include(x => x.TestTemplate).ThenInclude(x => x.Settings).FirstOrDefault(t => t.Id == x.Data.TestId).TestTemplate.Settings.PossibleChangeAnswer, () =>
                {
                    RuleFor(x => x.Data)
                        .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x.TestId).TestQuestions.First(q => q.Index == x.QuestionIndex).AnswerStatus != AnswerStatusEnum.Answered)
                        .WithErrorCode(ValidationCodes.CANT_CHANGE_ANSWER);
                });

                When(x => !appDbContext.Tests.Include(x => x.TestTemplate).ThenInclude(x => x.Settings).First(t => t.Id == x.Data.TestId).TestTemplate.Settings.PossibleGetToSkipped, () =>
                {
                    RuleFor(x => x.Data)
                        .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x.TestId).TestQuestions.First(q => q.Index == x.QuestionIndex).AnswerStatus != AnswerStatusEnum.Skipped)
                        .WithErrorCode(ValidationCodes.CANT_RETURN_TO_QUESTION);
                });
            });

            RuleFor(x => x.Data.TestId)
                .SetValidator(x => new TestCurrentUserValidator(userProfileService, appDbContext, ValidationCodes.INVALID_USER));
        }
    }
}
