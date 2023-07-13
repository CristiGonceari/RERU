using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas.AddKinshipRelationCriminalData
{
    public class AddKinshipRelationCriminalDataCommandValidator : AbstractValidator<AddKinshipRelationCriminalDataCommand>
    {
        private readonly AppDbContext _appDbContext;
        public AddKinshipRelationCriminalDataCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {

            _appDbContext = appDbContext;

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationCriminalDataValidator(appDbContext, currentUserProvider));

            RuleFor(x => x.Data.ContractorId)
                .Custom(CheckIfUserProfileExistentKinshipRelationDate);
        }

        private void CheckIfUserProfileExistentKinshipRelationDate(int contractorId, CustomContext context)
        {
            var exist = _appDbContext.KinshipRelationCriminalDatas.FirstOrDefault(x => x.ContractorId == contractorId);

            if (exist != null)
            {
                context.AddFail(ValidationCodes.EXISTENT_KINSHIP_RELATION_IN_SISTEM, ValidationMessages.InvalidReference);
            }
        }
       
    }
}
