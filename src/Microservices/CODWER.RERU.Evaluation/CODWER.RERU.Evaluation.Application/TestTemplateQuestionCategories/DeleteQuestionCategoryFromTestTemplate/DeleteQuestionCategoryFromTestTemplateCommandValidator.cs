using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.DeleteQuestionCategoryFromTestType
{
    public class DeleteQuestionCategoryFromTestTemplateCommandValidator : AbstractValidator<DeleteQuestionCategoryFromTestTemplateCommand>
    {
        public DeleteQuestionCategoryFromTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<TestTypeQuestionCategory>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));
        }
    }
}
