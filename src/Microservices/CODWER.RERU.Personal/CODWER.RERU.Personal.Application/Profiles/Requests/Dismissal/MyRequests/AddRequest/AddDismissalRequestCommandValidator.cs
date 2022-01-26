using System;
using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.AddRequest
{
    public class AddDismissalRequestCommandValidator : AbstractValidator<AddDismissalRequestCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddDismissalRequestCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            RuleFor(x => x.From)
                .Must(x => x.Date >= DateTime.Now.Date)
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x).Custom((data, c) => 
                CheckPositions(contractorId, c));
        }

        private void CheckPositions(int contractorId, CustomContext context)
        {
            var position = _appDbContext.Positions
                .Include(x => x.DismissalRequests)
                .OrderByDescending(x => x.FromDate)
                .FirstOrDefault(x => x.ContractorId == contractorId);

            if (position == null)
            {
                context.AddFail(ValidationCodes.POSITION_NOT_FOUND);
                return;
            }

            if (position.DismissalRequests.Any(x => x.Status == StageStatusEnum.Approved))
            {
                context.AddFail(ValidationCodes.EXISTENT_RECORD);
            }
        }
    }
}
