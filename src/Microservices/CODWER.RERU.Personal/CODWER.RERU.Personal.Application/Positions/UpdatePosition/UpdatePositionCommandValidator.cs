﻿using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Positions.UpdatePosition
{
    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<Position>(appDbContext,ValidationCodes.POSITION_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data).SetValidator(new PositionValidator(appDbContext));
        }
    }
}