﻿using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.DeleteMySolicitedPosition
{
    public class DeleteMySolicitedPositionCommandValidator : AbstractValidator<DeleteMySolicitedPositionCommand>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteMySolicitedPositionCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<SolicitedVacantPosition>(appDbContext, ValidationCodes.INVALID_SOLICITED_POSITION,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Id)
                .Must(x => IsNew(x))
                .WithErrorCode(ValidationCodes.ONLY_NEW_SOLICITED_TEST_CAN_BE_DELETED);
        }

        private bool IsNew(int id)
        {
            var solicitedTest = _appDbContext.SolicitedVacantPositions.FirstOrDefault(x => x.Id == id);

            if(solicitedTest.SolicitedPositionStatus == SolicitedPositionStatusEnum.New)
            {
                return true;
            }

            return false;
        }
    }
}