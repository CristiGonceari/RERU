using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.DeleteTestTemplate
{
    public class DeleteTestTemplateCommandValidator : AbstractValidator<DeleteTestTemplateCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteTestTemplateCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<TestTemplate>(appDbContext, ValidationCodes.INVALID_TEST_TEMPLATE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .Must(x => appDbContext.TestTemplates.First(tt => tt.Id == x).Status == TestTemplateStatusEnum.Draft)
                .WithErrorCode(ValidationCodes.ONLY_INACTIVE_TEST_CAN_BE_DELETED);
        }
    }
}