using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.ApproveRejectRequest
{
    public class ApproveRejectRequestCommandValidator : AbstractValidator<ApproveRejectRequestCommand>
    {
        private readonly AppDbContext _appDbContext;

        public ApproveRejectRequestCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<DismissalRequest>(appDbContext,
                    ValidationCodes.DISMISS_REQUEST_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x).Custom((data, c) =>
                CheckSubordinateRequest(contractorId, data.Data.Id, c));
        }

        private void CheckSubordinateRequest(int contractorId, int requestId, CustomContext context)
        {
            var request = _appDbContext.DismissalRequests
                .Include(x => x.Contractor)
                .ThenInclude(x => x.Contracts)
                .FirstOrDefault(x => x.Id == requestId);

            if (!request.Contractor.Contracts.Any())
            {
                context.AddFail(ValidationCodes.CONTRACT_NOT_FOUND, ValidationMessages.InvalidReference);
                return;
            }

            if (request.Contractor.Contracts.LastOrDefault()?.SuperiorId != contractorId)
            {
                context.AddFail(ValidationCodes.INVALID_INPUT, ValidationMessages.InvalidInput);
                return;
            }

            if (request.Status != StageStatusEnum.New)
            {
                context.AddFail(ValidationCodes.INVALID_INPUT, ValidationMessages.InvalidInput);
            }
        }
    }
}
