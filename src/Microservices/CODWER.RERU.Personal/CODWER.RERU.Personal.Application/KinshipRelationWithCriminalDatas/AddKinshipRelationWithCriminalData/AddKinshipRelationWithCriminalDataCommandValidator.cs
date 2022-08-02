using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas.AddKinshipRelationWithCriminalData
{
    public class AddKinshipRelationWithCriminalDataCommandValidator : AbstractValidator<AddKinshipRelationWithCriminalDataCommand>
    {
        private readonly AppDbContext _appDbContext;
        public AddKinshipRelationWithCriminalDataCommandValidator(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;

            RuleFor(x => x.Data)
                .SetValidator(new KinshipRelationWithCriminalDataValidator(appDbContext));

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
