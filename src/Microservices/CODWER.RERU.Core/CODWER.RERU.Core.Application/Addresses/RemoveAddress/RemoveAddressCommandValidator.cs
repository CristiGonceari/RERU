using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Addresses.RemoveAddress
{
    public class RemoveAddressCommandValidator : AbstractValidator<RemoveAddressCommand>
    {
        public RemoveAddressCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Address>(appDbContext, ValidationCodes.ADDRESS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
