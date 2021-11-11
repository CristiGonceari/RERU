using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.SaveTestQuestion
{
    public class SaveTestQuestionCommandValidator : AbstractValidator<SaveTestQuestionCommand>
    {
        public SaveTestQuestionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .Must(x => appDbContext.TestQuestions
                        .Any(t => t.Id == appDbContext.Tests
                            .FirstOrDefault(ts => ts.Id == x.TestId)
                            .TestQuestions
                            .FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
                .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

            RuleFor(x => x.Data.Status)
                .NotNull()
                .IsInEnum()
                .WithErrorCode(ValidationCodes.INVALID_ANSWER_STATUS);

            When(x => appDbContext.Tests.Include(x => x.TestType).ThenInclude(x => x.Settings).FirstOrDefault(t => t.Id == x.Data.TestId).TestType.Settings.ShowManyQuestionPerPage == false, () => 
            {
                When(x => !appDbContext.Tests.Include(x => x.TestType).ThenInclude(x => x.Settings).FirstOrDefault(t => t.Id == x.Data.TestId).TestType.Settings.PossibleChangeAnswer, () =>
                {
                    RuleFor(x => x.Data)
                        .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x.TestId).TestQuestions.First(q => q.Index == x.QuestionIndex).AnswerStatus != AnswerStatusEnum.Answered)
                        .WithErrorCode(ValidationCodes.CANT_CHANGE_ANSWER);
                });

                When(x => !appDbContext.Tests.Include(x => x.TestType).ThenInclude(x => x.Settings).First(t => t.Id == x.Data.TestId).TestType.Settings.PossibleGetToSkipped, () =>
                {
                    RuleFor(x => x.Data)
                        .Must(x => appDbContext.Tests.Include(x => x.TestQuestions).First(t => t.Id == x.TestId).TestQuestions.First(q => q.Index == x.QuestionIndex).AnswerStatus != AnswerStatusEnum.Skipped)
                        .WithErrorCode(ValidationCodes.CANT_RETURN_TO_QUESTION);
                });
            });
        }
    }
}
