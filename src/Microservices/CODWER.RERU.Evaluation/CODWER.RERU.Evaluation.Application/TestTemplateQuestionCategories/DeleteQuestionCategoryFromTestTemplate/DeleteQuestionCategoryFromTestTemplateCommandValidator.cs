using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.DeleteQuestionCategoryFromTestTemplate
{
    public class DeleteQuestionCategoryFromTestTemplateCommandValidator : AbstractValidator<DeleteQuestionCategoryFromTestTemplateCommand>
    {
        public DeleteQuestionCategoryFromTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<TestTemplateQuestionCategory>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));
        }
    }
}
