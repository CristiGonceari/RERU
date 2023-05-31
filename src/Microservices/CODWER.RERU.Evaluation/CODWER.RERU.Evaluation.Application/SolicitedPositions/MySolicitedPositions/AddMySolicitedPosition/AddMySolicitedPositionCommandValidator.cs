using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommandValidator : AbstractValidator<AddMySolicitedPositionCommand>
    {
        private readonly IUserProfileService _userProfileService;

        public AddMySolicitedPositionCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;

            RuleFor(x => x.Data.CandidatePositionId)
                .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data)
                .MustAsync(async (solicitedPosition, cancellationToken) => await IsCandidate(solicitedPosition, appDbContext, userProfileService))
                .WithErrorCode(ValidationCodes.CAN_APPLY_POSITION_IF_YOU_ARE_EVALUATOR_EVENT);
        }

        private async Task<bool> IsCandidate(AddEditSolicitedPositionDto solicitedPosition, 
                                            AppDbContext appDbContext, 
                                            IUserProfileService userProfileService)
        {
            var myUserProfile = await userProfileService.GetCurrentUserProfileDto();

            var eventIds = await appDbContext.EventVacantPositions
                .Where(ev => ev.CandidatePositionId == solicitedPosition.CandidatePositionId)
                .Select(ev => ev.EventId)
                .ToListAsync();

            bool isEvaluator = await appDbContext.EventEvaluators
                .AnyAsync(ee => ee.EvaluatorId == myUserProfile.Id && eventIds.Contains(ee.EventId));

            return !isEvaluator;
        }
    }
}
