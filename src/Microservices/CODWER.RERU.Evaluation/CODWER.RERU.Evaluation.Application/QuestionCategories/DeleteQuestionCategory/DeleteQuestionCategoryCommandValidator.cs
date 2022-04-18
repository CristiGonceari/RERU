using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.DeleteQuestionCategory
{
    public class DeleteQuestionCategoryCommandValidator : AbstractValidator<DeleteQuestionCategoryCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteQuestionCategoryCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r.Id)
                .Must(x => appDbContext.QuestionCategories.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_CATEGORY);

            RuleFor(r => r.Id)
                .Must(x => IsInActiveTest(x))
                .WithErrorCode(ValidationCodes.CATEGORY_USED_IN_ACTIVE_TESTS_CANT_BE_DELETED);
        }

        private bool IsInActiveTest(int id)
        {
            var testTemplates = _appDbContext.TestTemplates
                .Include(tt => tt.TestTemplateQuestionCategories)
                .Where(tt => tt.TestTemplateQuestionCategories.Any(q => q.QuestionCategoryId == id))
                .Any(tt => tt.Status == TestTemplateStatusEnum.Active);

            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestTemplate)
                .Where(t => t.TestTemplate.TestTemplateQuestionCategories.Any(q => q.QuestionCategoryId == id))
                .Any(t => t.TestStatus != TestStatusEnum.Terminated && t.TestStatus != TestStatusEnum.Verified);

            if (testTemplates || tests)
            {
                return false;
            }

            return true;
        }
    }
}
