using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using CVU.ERP.Common;
using CVU.ERP.Common.Extensions;

namespace CODWER.RERU.Personal.Application.Contractors.UpdateContractor
{
    public class UpdateContractorCommandValidator : AbstractValidator<UpdateContractorCommand>
    {
        private readonly AppDbContext _appDbContext;

        public UpdateContractorCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new ContractorValidator(appDbContext, dateTime));

            //RuleFor(x => x.Data.Idnp)
            //   .Custom(CheckIfUniqueIdnpOnCreate);
        }
        //private void CheckIfUniqueIdnpOnCreate(string idnp, CustomContext context)
        //{
        //    var exist = _appDbContext.UserProfiles
        //        .Any(x => x.Idnp == idnp);

        //    if (exist)
        //    {
        //        context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
        //    }
        //}
    }
}
