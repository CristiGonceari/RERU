using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using System.Linq;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using Microsoft.EntityFrameworkCore;

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
            var testTypes = _appDbContext.TestTemplates
                .Include(tt => tt.TestTypeQuestionCategories)
                .Where(tt => tt.TestTypeQuestionCategories.Any(q => q.QuestionCategoryId == id))
                .Any(tt => tt.Status == TestTypeStatusEnum.Active);

            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestTemplate)
                .Where(t => t.TestTemplate.TestTypeQuestionCategories.Any(q => q.QuestionCategoryId == id))
                .Any(t => t.TestStatus != TestStatusEnum.Terminated && t.TestStatus != TestStatusEnum.Verified);

            if (testTypes || tests)
            {
                return false;
            }

            return true;
        }
    }
}
