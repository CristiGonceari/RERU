using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions
{
    public class GenerateTestQuestionsCommandValidator : AbstractValidator<GenerateTestQuestionsCommand>
    {
        private readonly AppDbContext _appDbContext;

        public GenerateTestQuestionsCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext.NewInstance();

            RuleFor(x => x.TestId)
                .SetValidator(x => new ItemMustExistValidator<Test>(_appDbContext, ValidationCodes.INVALID_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.TestId)
                .Must(x => _appDbContext.Tests.Include(t => t.TestQuestions).FirstOrDefault(t => t.Id == x).TestQuestions?.Count == 0)
                .WithErrorCode(ValidationCodes.QUESTIONS_ARE_GENERATED_FOR_THIS_TEST);

            RuleForEach(x => _appDbContext.Tests
                .Include(x => x.TestTemplate)
                    .ThenInclude(x => x.TestTemplateQuestionCategories)
                .First(t => t.Id == x.TestId)
                    .TestTemplate.TestTemplateQuestionCategories)
                    .Must(x => x.QuestionCount <= _appDbContext.QuestionCategories
                        .Include(x => x.QuestionUnits)
                        .FirstOrDefault(c => c.Id == x.QuestionCategoryId)
                        .QuestionUnits
                            .Count(q => q.Status == QuestionUnitStatusEnum.Active))
                .WithErrorCode(ValidationCodes.INSUFFICIENT_QUESTIONS_IN_CATEGORY);

        }
    }
}
