using System;
using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DismissalRequests.AddDismissalRequest
{
    public class AddDismissalRequestCommandValidator: AbstractValidator<AddDismissalRequestCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddDismissalRequestCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.From)
                .Must(x => x.Date >= DateTime.Now.Date)
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x).Custom((data, c) =>
                CheckPositions(data.Data.ContractorId, c));
        }

        private void CheckPositions(int contractorId, CustomContext context)
        {
            var position = _appDbContext.Positions
                .Include(x => x.DismissalRequests)
                .OrderByDescending(x => x.FromDate)
                .FirstOrDefault(x => x.ContractorId == contractorId);

            if (position == null)
            {
                context.AddFail(ValidationCodes.POSITION_NOT_FOUND, ValidationMessages.NotFound);
                return;
            }

            if (position.DismissalRequests.Any(x => x.Status == StageStatusEnum.Approved))
            {
                context.AddFail(ValidationCodes.EXISTENT_RECORD, ValidationMessages.InvalidInput);
            }
        }
    }
}
