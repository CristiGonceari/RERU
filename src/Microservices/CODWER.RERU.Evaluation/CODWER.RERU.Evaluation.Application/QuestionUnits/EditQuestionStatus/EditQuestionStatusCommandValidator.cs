using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus
{
    public class EditQuestionStatusCommandValidator : AbstractValidator<EditQuestionStatusCommand>
    {
        public EditQuestionStatusCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.QuestionId)
                    .SetValidator(x => new ItemMustExistValidator<QuestionUnit>(appDbContext, ValidationCodes.INVALID_QUESTION,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.Status)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_STATUS);

                When(x => appDbContext.QuestionUnits.Where(q => q.Id == x.Data.QuestionId).Any(q =>
                        (q.QuestionType == QuestionTypeEnum.MultipleAnswers || q.QuestionType == QuestionTypeEnum.OneAnswer) && q.Status == QuestionUnitStatusEnum.Draft), () =>
                {
                    RuleFor(x => x.Data)
                        .Must(x => appDbContext.Options.Any(o => o.QuestionUnitId == x.QuestionId))
                        .WithErrorCode(ValidationCodes.INVALID_OPTION);

                    RuleFor(x => x.Data)
                        .Must(x => appDbContext.Options.Where(o => o.QuestionUnitId == x.QuestionId).Any(o => o.IsCorrect))
                        .WithErrorCode(ValidationCodes.EMPTY_CORRECT_OPTION);
                });

                When(x => 
                    appDbContext.Tests.Include(x => x.TestQuestions).ThenInclude(x => x.QuestionUnit)
                              .Where(t => t.TestQuestions.Any(tq => tq.QuestionUnitId == x.Data.QuestionId)).Any(s => s.TestStatus > TestStatusEnum.AlowedToStart) &&
                    appDbContext.Tests.Include(x => x.TestQuestions).ThenInclude(x => x.QuestionUnit)
                              .Where(t => t.TestQuestions.Any(tq => tq.QuestionUnitId == x.Data.QuestionId)).Any(s => s.TestStatus <= TestStatusEnum.Verified), () =>
                {
                    RuleFor(x => x.Data.Status)
                        .Must(x => x == QuestionUnitStatusEnum.Inactive)
                        .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TEMPLATE);
                });
            });
        }
    }
}
