using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

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
