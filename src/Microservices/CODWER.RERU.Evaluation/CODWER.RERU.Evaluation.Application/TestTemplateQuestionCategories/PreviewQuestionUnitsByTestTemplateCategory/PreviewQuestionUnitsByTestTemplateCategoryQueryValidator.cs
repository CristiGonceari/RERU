using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplateQuestionCategories;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.PreviewQuestionUnitsByTestTemplateCategory
{
    public class PreviewQuestionUnitsByTestTemplateCategoryQueryValidator : AbstractValidator<PreviewQuestionUnitsByTestTemplateCategoryQuery>
    {
        private readonly AppDbContext _appDbContext;

        public PreviewQuestionUnitsByTestTemplateCategoryQueryValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.TestTemplateId)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.TestTemplateId)
                    .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTemplateStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                RuleFor(x => x.Data.CategoryId)
                    .SetValidator(x => new ItemMustExistValidator<QuestionCategory>(appDbContext, ValidationCodes.INVALID_CATEGORY,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data)
                    .Must(x => IsPoll(x))
                    .WithErrorCode(ValidationCodes.POLLS_ACCEPTS_ONLY_ONE_ANSWER_QUESTIONS);

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
                        .Must(x => appDbContext.QuestionCategories.Include(c => c.QuestionUnits).First(c => c.Id == x.CategoryId).QuestionUnits.Count >= (appDbContext.TestTemplates.First(w => w.Id == x.TestTemplateId).QuestionCount - appDbContext.TestTemplates.Include(w => w.TestTemplateQuestionCategories).First(w => w.Id == x.TestTemplateId).TestTemplateQuestionCategories.Sum(x => x.QuestionCount.Value)))
                        .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

                    When(r => r.Data.QuestionType.HasValue, () =>
                    {
                        RuleFor(r => r.Data)
                            .Must(x => appDbContext.QuestionCategories.Include(c => c.QuestionUnits).First(c => c.Id == x.CategoryId).QuestionUnits.Where(q => q.QuestionType == x.QuestionType.Value).Count() >= (appDbContext.TestTemplates.First(w => w.Id == x.TestTemplateId).QuestionCount - appDbContext.TestTemplates.Include(w => w.TestTemplateQuestionCategories).First(w => w.Id == x.TestTemplateId).TestTemplateQuestionCategories.Sum(x => x.QuestionCount.Value)))
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

        private bool IsPoll(QuestionCategoryPreviewDto data)
        {
            var testTemplate = _appDbContext.TestTemplates.FirstOrDefault(x => x.Id == data.TestTemplateId);
            var selectedQuestions = false;

            if (testTemplate != null && testTemplate.Mode == TestTemplateModeEnum.Poll)
            {
                if (data.SelectedQuestions != null)
                {
                    selectedQuestions = data.SelectedQuestions
                        .All(x => _appDbContext.QuestionUnits.FirstOrDefault(q => q.Id == x.Id).QuestionType == QuestionTypeEnum.OneAnswer);
                }
                
                var questions = _appDbContext.QuestionUnits
                    .Where(q => q.QuestionCategoryId == data.CategoryId)
                    .All(q => q.QuestionType == QuestionTypeEnum.OneAnswer);

                return selectedQuestions || questions || data.QuestionType == QuestionTypeEnum.OneAnswer;
            }

            return true;
        }
    }
}
