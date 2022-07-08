using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFiles
{
    public class GetSolicitedVacantPositionUserFilesQueryValidator : AbstractValidator<GetSolicitedVacantPositionUserFilesQuery>
    {
        public GetSolicitedVacantPositionUserFilesQueryValidator(AppDbContext appDbContext)
        {
            When(x => x.UserId != null, () =>
            {
                RuleFor(x => x.UserId.Value)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext,
                        ValidationCodes.INVALID_USER,
                        ValidationMessages.NotFound));
            });
            
            RuleFor(x => x.SolicitedVacantPositionId)
                .SetValidator(x => new ItemMustExistValidator<SolicitedVacantPosition>(appDbContext, ValidationCodes.INVALID_SOLICITED_VACANT_POSITION,
                    ValidationMessages.NotFound));
        }
    }
}
