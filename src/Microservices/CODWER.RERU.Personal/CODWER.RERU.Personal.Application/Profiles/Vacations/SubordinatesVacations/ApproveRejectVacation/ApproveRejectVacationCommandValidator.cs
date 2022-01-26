using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.SubordinatesVacations.ApproveRejectVacation
{
    public class ApproveRejectVacationCommandValidator : AbstractValidator<ApproveRejectVacationCommand>
    {
        private readonly AppDbContext _appDbContext;

        public ApproveRejectVacationCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            RuleFor(x => x.Data.VacationId)
                .SetValidator(new ItemMustExistValidator<Vacation>(appDbContext, ValidationCodes.VACATION_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x).Custom((data, c) =>
                CheckSubordinateVacation(contractorId, data.Data.VacationId, c));
        }

        private void CheckSubordinateVacation(int contractorId, int vacationId, CustomContext context)
        {
            var vacation = _appDbContext.Vacations
                .Include(x => x.Contractor)
                .ThenInclude(x=>x.Contracts)
                .FirstOrDefault(x => x.Id == vacationId);

            if (!vacation.Contractor.Contracts.Any())
            {
                context.AddFail(ValidationCodes.CONTRACT_NOT_FOUND, ValidationMessages.InvalidReference);
                return;
            }

            if (vacation?.Contractor.Contracts.LastOrDefault()?.SuperiorId != contractorId)
            {
                context.AddFail(ValidationCodes.INVALID_INPUT, ValidationMessages.InvalidInput);
                return;
            }

            if (vacation.Status != StageStatusEnum.New)
            {
                context.AddFail(ValidationCodes.INVALID_INPUT, ValidationMessages.InvalidInput);
            }
        }
    }
}
