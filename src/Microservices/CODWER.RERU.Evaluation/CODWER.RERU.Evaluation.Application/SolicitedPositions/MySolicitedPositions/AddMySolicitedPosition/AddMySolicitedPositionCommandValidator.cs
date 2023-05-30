using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommandValidator : AbstractValidator<AddMySolicitedPositionCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public AddMySolicitedPositionCommandValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;

            RuleFor(x => x.Data.CandidatePositionId)
                .SetValidator(x => new ItemMustExistValidator<CandidatePosition>(appDbContext, ValidationCodes.INVALID_POSITION,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.CandidatePositionId)
                .MustAsync(async (id, cancellationToken) => await IsCandidate(id))
                .WithErrorCode(ValidationCodes.CAN_APPLY_POSITION_IF_YOU_ARE_EVALUATOR_EVENT);
        }

        private async Task<bool> IsCandidate(int candidatePositionId)
        {
            var myUserProfile = await _userProfileService.GetCurrentUserProfileDto();

            var eventId = _appDbContext.EventVacantPositions
                                .FirstOrDefault(x => x.CandidatePositionId == candidatePositionId)?.EventId;

            if (eventId.HasValue)
            {
                bool isEvaluator = _appDbContext.EventEvaluators
                                .Any(ee => ee.EvaluatorId == myUserProfile.Id && ee.EventId == eventId.Value);

                return !isEvaluator;
            }

            return true;
        }
    }
}
