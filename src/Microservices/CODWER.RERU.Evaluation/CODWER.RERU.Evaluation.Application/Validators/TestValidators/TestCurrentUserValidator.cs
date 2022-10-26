using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Validators.TestValidators
{
    internal class TestCurrentUserValidator : AbstractValidator<int>
    {
        private readonly IUserProfileService _userProfileService;
        private readonly AppDbContext _appDbContext;

        public TestCurrentUserValidator(IUserProfileService userProfileService, AppDbContext appDbContext, string errorCode)
        {
            _userProfileService = userProfileService;
            _appDbContext = appDbContext;

            RuleFor(x => x)
                .MustAsync(async (id, cancellation) => await CheckTestCurrentUser(id))
                .WithErrorCode(errorCode);
        }

        private async Task<bool> CheckTestCurrentUser(int testId)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var test = _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefault(x => x.Id == testId);

            switch (test.TestTemplate.Mode)
            {
                case TestTemplateModeEnum.Test:
                case TestTemplateModeEnum.Poll:
                {
                    if (test.UserProfileId == currentUserId) return true;
                    break;
                }
                case TestTemplateModeEnum.Evaluation when test.EvaluatorId == currentUserId:
                    return true;
            }

            return false;
        }
    }
}
