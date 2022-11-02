using System;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestCategoryQuestions.GetTestCategoryQuestions
{
    public class TestCategoryQuestionsQueryValidator : AbstractValidator<TestCategoryQuestionsQuery>
    {
        private readonly AppDbContext _appDbContext;

        public TestCategoryQuestionsQueryValidator(IServiceProvider serviceProvider, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext.NewInstance();

            RuleFor(x => x.TestTemplateQuestionCategoryId)
                .SetValidator(x => new ItemMustExistValidator<TestTemplateQuestionCategory>(_appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE_QUESTION_CATEGORY,
                    ValidationMessages.InvalidReference));
        }
    }
}
