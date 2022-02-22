using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.AssignQuestionCategoryToTestTemplate
{
    public class AssignQuestionCategoryToTestTemplateCommandValidator : AbstractValidator<AssignQuestionCategoryToTestTemplateCommand>
    {
        public AssignQuestionCategoryToTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                    .NotNull()
                    .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.TestTemplateId)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.QuestionCategoryId)
                    .SetValidator(x => new ItemMustExistValidator<QuestionCategory>(appDbContext, ValidationCodes.INVALID_CATEGORY,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data)
                    .Must(x => appDbContext.TestTemplateQuestionCategories
                            .Where(q => q.TestTemplateId == x.TestTemplateId)
                            .Sum(s => s.QuestionCount) < appDbContext.TestTemplates.FirstOrDefault(t => t.Id == x.TestTemplateId).QuestionCount)
                    .WithErrorCode(ValidationCodes.QUESTION_COUNT_REACHED_THE_LIMIT);

                RuleFor(x => x.Data)
                    .Must(x => (appDbContext.TestTemplateQuestionCategories
                            .Where(q => q.TestTemplateId == x.TestTemplateId)
                            .Sum(s => s.QuestionCount) + x.QuestionCount) <= appDbContext.TestTemplates.FirstOrDefault(t => t.Id == x.TestTemplateId).QuestionCount)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                When(r => r.Data.QuestionCount.HasValue, () =>
                {
                    RuleFor(x => x.Data.QuestionCount)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT);

                    RuleFor(x => x.Data)
                    .Must(x => appDbContext.QuestionUnits.Where(qc => qc.QuestionCategoryId == x.QuestionCategoryId).Count() >= x.QuestionCount.Value)
                    .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

                    When(r => r.Data.TestCategoryQuestions.Count > 0, () =>
                    {
                        RuleFor(x => x.Data)
                        .Must(x => x.QuestionCount == x.TestCategoryQuestions.Count)
                        .WithErrorCode(ValidationCodes.MISMATCH_QUESTION_COUNT_AND_SELECTED);
                    });
                });

                When(r => !r.Data.QuestionCount.HasValue, () =>
                {
                    RuleFor(x => x.Data)
                    .Must(x => appDbContext.QuestionUnits.Where(qc => qc.QuestionCategoryId == x.QuestionCategoryId).Count() > 0)
                    .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);
                });

                When(r => r.Data.TimeLimit.HasValue, () =>
                {
                    RuleFor(x => x.Data.TimeLimit.Value)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_TIME);
                });

                When(r => r.Data.SelectionType == SelectionEnum.Select, () =>
                {
                    RuleFor(x => x.Data.TestCategoryQuestions)
                    .NotNull()
                    .Must(x => x.Count() > 0)
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);
                });

                When(r => r.Data.SequenceType == SequenceEnum.Strict, () =>
                {
                    RuleFor(x => x.Data.TestCategoryQuestions)
                    .NotNull()
                    .Must(x => x.Count() > 0)
                    .WithErrorCode(ValidationCodes.INVALID_RECORD);

                    RuleFor(x => x.Data.TestCategoryQuestions)
                   .Must(x => x.Select(s => s.Index).Distinct().Count() == x.Count)
                   .WithErrorCode(ValidationCodes.INVALID_RECORD);

                    RuleFor(x => x.Data.SelectionType)
                    .Must(x => x == SelectionEnum.Select)
                    .WithErrorCode(ValidationCodes.INVALID_COMBINATION);
                });
            });
        }
    }
}
