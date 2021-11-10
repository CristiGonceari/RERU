using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.PreviewQuestionUnitsByTestTypeCategory
{
    public class PreviewQuestionUnitsByTestTypeCategoryQueryValidator : AbstractValidator<PreviewQuestionUnitsByTestTypeCategoryQuery>
    {
        public PreviewQuestionUnitsByTestTypeCategoryQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.TestTypeId)
                    .GreaterThan(0)
                    .Must(x => appDbContext.TestTypes.Any(t => t.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);

                RuleFor(x => x.Data.TestTypeId)
                    .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                RuleFor(r => r.Data.CategoryId)
                    .GreaterThan(0)
                    .Must(x => appDbContext.QuestionCategories.Any(t => t.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_CATEGORY);

                RuleFor(r => r.Data.SelectionType)
                    .NotNull()
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_SELECTION_TYPE);

                RuleFor(r => r.Data.SequenceType)
                    .NotNull()
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.INVALID_SEQUENCE_TYPE);

                When(r => r.Data.QuestionType.HasValue, () =>
                {
                    RuleFor(r => r.Data.QuestionType)
                        .IsInEnum()
                        .WithErrorCode(ValidationCodes.EMPTY_QUESTION_TYPE);
                });

                When(r => r.Data.QuestionCount.HasValue, () =>
                {
                    RuleFor(r => r.Data)
                        .Must(x => appDbContext.QuestionCategories.Include(c => c.QuestionUnits).First(c => c.Id == x.CategoryId).QuestionUnits.Where(q => q.Status == QuestionUnitStatusEnum.Active).Count() >= x.QuestionCount)
                        .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

                    RuleFor(r => r.Data.QuestionCount)
                        .GreaterThan(0)
                        .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                    When(r => r.Data.QuestionType.HasValue, () =>
                    {
                        RuleFor(r => r.Data)
                            .Must(x => appDbContext.QuestionCategories.Include(c => c.QuestionUnits).First(c => c.Id == x.CategoryId).QuestionUnits.Where(q => q.QuestionType == x.QuestionType.Value && q.Status == QuestionUnitStatusEnum.Active).Count() >= x.QuestionCount)
                            .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);
                    });
                });

                When(r => !r.Data.QuestionCount.HasValue, () =>
                {

                    RuleFor(r => r.Data)
                        .Must(x => appDbContext.QuestionCategories.Include(c => c.QuestionUnits).First(c => c.Id == x.CategoryId).QuestionUnits.Count >= (appDbContext.TestTypes.First(w => w.Id == x.TestTypeId).QuestionCount - appDbContext.TestTypes.Include(w => w.TestTypeQuestionCategories).First(w => w.Id == x.TestTypeId).TestTypeQuestionCategories.Sum(x => x.QuestionCount.Value)))
                        .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

                    When(r => r.Data.QuestionType.HasValue, () =>
                    {
                        RuleFor(r => r.Data)
                            .Must(x => appDbContext.QuestionCategories.Include(c => c.QuestionUnits).First(c => c.Id == x.CategoryId).QuestionUnits.Where(q => q.QuestionType == x.QuestionType.Value).Count() >= (appDbContext.TestTypes.First(w => w.Id == x.TestTypeId).QuestionCount - appDbContext.TestTypes.Include(w => w.TestTypeQuestionCategories).First(w => w.Id == x.TestTypeId).TestTypeQuestionCategories.Sum(x => x.QuestionCount.Value)))
                            .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);
                    });
                });

                When(r => r.Data.SelectionType == SelectionEnum.Select || r.Data.SequenceType == SequenceEnum.Strict, () =>
                {
                    RuleFor(r => r.Data.SelectedQuestions)
                        .NotNull()
                        .Must(x => x.Count > 0)
                        .WithErrorCode(ValidationCodes.INVALID_RECORD);

                    RuleFor(r => r.Data.SelectedQuestions)
                        .Must(x => appDbContext.QuestionUnits.Any(q => x.Select(s => s.Id).Contains(q.Id)))
                        .WithErrorCode(ValidationCodes.INVALID_QUESTION);

                    When(r => r.Data.SelectionType == SelectionEnum.Select, () =>
                    {
                        RuleFor(r => r.Data)
                            .Must(x => x.QuestionCount.HasValue && x.QuestionCount == x.SelectedQuestions.Distinct().Count())
                            .WithErrorCode(ValidationCodes.QUESTION_COUNT_MUST_BE_EQUAL_TO_SELECTED_COUNT);
                    });

                    When(r => r.Data.SequenceType == SequenceEnum.Strict, () =>
                    {
                        RuleFor(r => r.Data)
                            .Must(x => x.QuestionCount.HasValue && x.QuestionCount == x.SelectedQuestions.Distinct().Count())
                            .WithErrorCode(ValidationCodes.QUESTION_COUNT_MUST_BE_EQUAL_TO_SELECTED_COUNT);

                        RuleFor(r => r.Data)
                           .Must(x => x.SelectedQuestions.All(s => s.Index > 0) && x.SelectedQuestions.Select(i => i.Index).Distinct().Count() == x.SelectedQuestions.Count)
                           .WithErrorCode(ValidationCodes.INVALID_RECORD);
                    });
                });
            });
        }
    }
}
