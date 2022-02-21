using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.DeleteTestTemplate
{
    public class DeleteTestTemplateCommandValidator : AbstractValidator<DeleteTestTemplateCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<Data.Entities.TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == testTemplateStatusEnum.Draft)
                .WithErrorCode(ValidationCodes.ONLY_INACTIVE_TEST_CAN_BE_DELETED);
        }
    }
}