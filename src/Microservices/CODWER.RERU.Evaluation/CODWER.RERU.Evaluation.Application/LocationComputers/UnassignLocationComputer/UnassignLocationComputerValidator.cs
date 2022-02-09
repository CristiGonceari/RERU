using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.LocationComputers.UnassignLocationComputer
{
    public class UnassignLocationComputerValidator : AbstractValidator<UnassignLocationComputerCommand>
    {
        public UnassignLocationComputerValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.LocationClientId)
                 .SetValidator(x => new ItemMustExistValidator<LocationClient>(appDbContext, ValidationCodes.INVALID_RECORD,
                        ValidationMessages.InvalidReference));
        }
    }
}
