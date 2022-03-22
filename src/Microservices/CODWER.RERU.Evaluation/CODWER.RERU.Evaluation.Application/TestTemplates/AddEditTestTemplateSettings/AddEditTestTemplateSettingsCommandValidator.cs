using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddEditTestTemplateSettings
{
    public class AddEditTestTemplateSettingsCommandValidator : AbstractValidator<AddEditTestTemplateSettingsCommand>
    {
        private readonly AppDbContext _appDbContext;
        public AddEditTestTemplateSettingsCommandValidator(AppDbContext appDbContext)
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

                When(r => appDbContext.TestTemplates.First(tt => tt.Id == r.Data.TestTemplateId).Mode == TestTemplateModeEnum.Poll, () =>
                {
                    RuleFor(x => x.Data.CanViewPollProgress)
                        .NotNull()
                        .WithErrorCode(ValidationCodes.INVALID_POLL_SETTINGS);
                });
               
                RuleFor(x => x.Data)
                    .Must(x => CheckFormulas(x))
                    .WithErrorCode(ValidationCodes.EMPTY_FORMULA);

                When(r => r.Data.ShowManyQuestionPerPage == true, () =>
                {
                    RuleFor(x => x.Data.QuestionsCountPerPage)
                        .Must(x => x.HasValue && x.Value > 0)
                        .WithErrorCode(ValidationCodes.INVALID_QUESTION_COUNT_PER_PAGE);
                });

                When(r => r.Data.MaxErrors.HasValue, () =>
                {
                    RuleFor(x => x.Data.MaxErrors.Value)
                        .GreaterThan(0)
                        .WithErrorCode(ValidationCodes.INVALID_MAX_ERRORS);
                });
            });
        }

        private bool CheckFormulas(TestTemplateSettingsDto data)
        {
            var testTemplate = _appDbContext.TestTemplates.First(tt => tt.Id == data.TestTemplateId);

            if (testTemplate.Mode == TestTemplateModeEnum.Test)
            {
                if (data.FormulaForMultipleAnswers != null && data.FormulaForOneAnswer != null)
                {
                    return true;
                }
            } 
            else if (testTemplate.Mode == TestTemplateModeEnum.Poll)
            {
                return true;
            }

            return false;
        }
    }
}
