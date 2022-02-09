using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddEditTestTypeSettings
{
    public class AddEditTestTemplateSettingsCommandValidator : AbstractValidator<AddEditTestTemplateSettingsCommand>
    {
        public AddEditTestTemplateSettingsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.TestTypeId)
                    .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                        ValidationMessages.InvalidReference));

                RuleFor(x => x.Data.TestTypeId)
                    .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft)
                    .WithErrorCode(ValidationCodes.ONLY_PENDING_TEST_CAN_BE_CHANGED);

                When(r => appDbContext.TestTemplates.First(tt => tt.Id == r.Data.TestTypeId).Mode == TestTypeModeEnum.Poll, () =>
                {
                    RuleFor(x => x.Data.CanViewPollProgress)
                        .NotNull()
                        .WithErrorCode(ValidationCodes.INVALID_POLL_SETTINGS);
                });

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
    }
}
