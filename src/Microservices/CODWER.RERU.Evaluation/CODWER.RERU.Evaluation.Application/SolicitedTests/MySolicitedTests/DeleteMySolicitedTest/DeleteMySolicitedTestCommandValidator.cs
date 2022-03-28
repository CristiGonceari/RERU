using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.DeleteMySolicitedTest
{
    public class DeleteMySolicitedTestCommandValidator : AbstractValidator<DeleteMySolicitedTestCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteMySolicitedTestCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<SolicitedTest>(appDbContext, ValidationCodes.INVALID_SOLICITED_TEST,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .Must(x => IsNew(x))
                .WithErrorCode(ValidationCodes.ONLY_NEW_SOLICITED_TEST_CAN_BE_DELETED);
        }

        private bool IsNew(int id)
        {
            var solicitedTest = _appDbContext.SolicitedTests.FirstOrDefault(x => x.Id == id);

            if(solicitedTest.SolicitedTestStatus == SolicitedTestStatusEnum.New)
            {
                return true;
            }

            return false;
        }
    }
}
