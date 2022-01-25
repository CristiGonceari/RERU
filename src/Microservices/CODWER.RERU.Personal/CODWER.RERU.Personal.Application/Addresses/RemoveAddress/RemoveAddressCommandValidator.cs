﻿using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Addresses.RemoveAddress
{
    public class RemoveAddressCommandValidator : AbstractValidator<RemoveAddressCommand>
    {
        public RemoveAddressCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Address>(appDbContext,ValidationCodes.ADDRESS_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
