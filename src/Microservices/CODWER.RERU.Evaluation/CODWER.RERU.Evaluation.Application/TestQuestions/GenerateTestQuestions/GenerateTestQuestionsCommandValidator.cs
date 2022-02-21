using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions
{
    public class GenerateTestQuestionsCommandValidator : AbstractValidator<GenerateTestQuestionsCommand>
    {
        public GenerateTestQuestionsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Must(x => appDbContext.Tests.Include(t => t.TestQuestions).FirstOrDefault(t => t.Id == x).TestQuestions?.Count == 0)
                .WithErrorCode(ValidationCodes.QUESTIONS_ARE_GENERATED_FOR_THIS_TEST);

            RuleForEach(x => appDbContext.Tests
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.testTemplateQuestionCategories)
                .First(t => t.Id == x.TestId)
                .TestTemplate.testTemplateQuestionCategories)
                .Must(x => x.QuestionCount <= appDbContext.QuestionCategories.Include(x => x.QuestionUnits).FirstOrDefault(c => c.Id == x.QuestionCategoryId).QuestionUnits.Where(q => q.Status == QuestionUnitStatusEnum.Active).Count())
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

            When(x => appDbContext.Tests.Include(x => x.TestTemplate)
                .First(t => t.Id == x.TestId)
                .TestTemplate.Mode == testTemplateModeEnum.Test, () =>
                {
                    When(x => !appDbContext.Tests
                         .Include(x => x.TestTemplate)
                             .ThenInclude(x => x.Settings)
                         .First(t => t.Id == x.TestId)
                         .TestTemplate.Settings.StartWithoutConfirmation, () =>
                         {
                             RuleFor(x => x.TestId)
                             .Must(x => appDbContext.Tests.First(t => t.Id == x).TestStatus == TestStatusEnum.AlowedToStart)
                             .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
                         });

                    When(x => !appDbContext.Tests
                        .Include(x => x.TestTemplate)
                            .ThenInclude(x => x.Settings)
                        .First(t => t.Id == x.TestId)
                        .TestTemplate.Settings.StartBeforeProgrammation, () =>
                        {
                            RuleFor(x => x.TestId)
                            .Must(x => appDbContext.Tests.First(t => t.Id == x).ProgrammedTime <= DateTime.Now)
                            .WithErrorCode(ValidationCodes.WAIT_THE_START_TIME);
                        });

                    When(x => !appDbContext.Tests
                        .Include(x => x.TestTemplate)
                            .ThenInclude(x => x.Settings)
                        .First(t => t.Id == x.TestId)
                        .TestTemplate.Settings.StartBeforeProgrammation, () =>
                        {
                            RuleFor(x => x.TestId)
                            .Must(x => appDbContext.Tests.First(t => t.Id == x).ProgrammedTime <= DateTime.Now)
                            .WithErrorCode(ValidationCodes.WAIT_THE_START_TIME);
                        });

                    When(x => appDbContext.Tests.First(t => t.Id == x.TestId).TestPassStatus.HasValue, () =>
                    {
                        RuleFor(x => x.TestId)
                        .Must(x => appDbContext.Tests.First(t => t.Id == x).TestPassStatus.Value == TestPassStatusEnum.Allowed)
                        .WithErrorCode(ValidationCodes.NEED_ADMIN_CONFIRMATION);
                    });

                    When(x => appDbContext.Tests.First(t => t.Id == x.TestId).EventId.HasValue, () =>
                    {
                        RuleFor(x => x.TestId)
                        .Must(x => appDbContext.Tests.Include(x => x.Event).First(t => t.Id == x).Event.TillDate > DateTime.Now)
                        .WithErrorCode(ValidationCodes.FINISHED_EVENT);
                    });
                });
        }
    }
}
