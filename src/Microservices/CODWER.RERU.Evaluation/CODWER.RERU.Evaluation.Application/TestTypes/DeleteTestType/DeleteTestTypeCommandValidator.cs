using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.TestTypes.DeleteTestType
{
    public class DeleteTestTypeCommandValidator : AbstractValidator<DeleteTestTypeCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteTestTypeCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .Must(x => appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Draft ||
                           appDbContext.TestTypes.First(tt => tt.Id == x).Status == TestTypeStatusEnum.Canceled)
                .WithErrorCode(ValidationCodes.ONLY_INACTIVE_TEST_CAN_BE_DELETED);
        }
    }
}