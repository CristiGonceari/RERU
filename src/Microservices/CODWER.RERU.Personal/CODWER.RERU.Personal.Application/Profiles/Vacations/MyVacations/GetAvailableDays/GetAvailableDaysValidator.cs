using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CVU.ERP.Common.Extensions;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetAvailableDays
{
    public class GetAvailableDaysValidator : AbstractValidator<GetAvailableDays>
    {
        private readonly AppDbContext _appDbContext;

        public GetAvailableDaysValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            RuleFor(x => x)
                .Custom((data, c) => CheckExistentContractsAndPositions(contractorId,c));
        }

        private void CheckExistentContractsAndPositions(int contractorId, CustomContext c)
        {
            var contractor = _appDbContext.Contractors
                .Include(x => x.Positions)
                .Include(x => x.Contracts)
                .FirstOrDefault(x => x.Id == contractorId);

            if (!contractor.Positions.Any() )
            {
                c.AddFail(ValidationCodes.POSITION_NOT_FOUND);
                return;
            }

            if (!contractor.Contracts.Any())
            {
                c.AddFail(ValidationCodes.POSITION_NOT_FOUND);
            }
        }
    }
}
