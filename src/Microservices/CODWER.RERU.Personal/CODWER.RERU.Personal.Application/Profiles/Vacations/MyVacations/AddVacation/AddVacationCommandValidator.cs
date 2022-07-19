using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using System.Linq;
using CVU.ERP.Common.Extensions;
using FluentValidation.Validators;
using System;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.AddVacation
{
    public class AddVacationCommandValidator : AbstractValidator<AddVacationCommand>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public AddVacationCommandValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            RuleFor(x => x.Data)
                .Custom((data, c) => CheckPositions(contractorId, c));

            RuleFor(x => x.Data.Mentions)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            When(x => x.Data.ToDate != null, () =>
            {
                RuleFor(x => x.Data)
                    .Must(x => x.ToDate > x.FromDate)
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationMessages.InvalidInput);
            });

            RuleFor(x => x.Data.VacationType)
                .IsInEnum()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.Data).Must(CheckVacationPeriod)
                .WithErrorCode(ValidationCodes.EXISTENT_RECORD)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Data)
                .Must(x => x.FromDate >= DateTime.Now.Date)
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            When(x => x.Data.VacationType == VacationType.Studies, () =>
            {
                RuleFor(x => x.Data.Institution)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);
            });

            When(x => x.Data.VacationType == VacationType.ChildCare, () =>
            {
                RuleFor(x => x.Data.ChildAge)
                    .NotEmpty()
                    .Must(x => x > 0)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);
            });
        }
        private bool CheckVacationPeriod(AddMyVacationDto vacation)
        {
            var currentUserProfile = _userProfileService.GetCurrentUserProfile();
            var contractorId = currentUserProfile.Result.Contractor.Id;

            var result = _appDbContext.Vacations
                .Where(x => x.ContractorId == contractorId)
                .All(x => (x.ToDate != null && vacation.ToDate != null && (vacation.FromDate > x.ToDate && vacation.ToDate > x.ToDate) || (vacation.FromDate < x.FromDate && vacation.ToDate < x.FromDate))
                       || (x.ToDate != null && vacation.ToDate == null && ((vacation.FromDate > x.ToDate) || (vacation.FromDate < x.FromDate)))
                       || (x.ToDate == null && vacation.ToDate != null && ((vacation.FromDate > x.FromDate) || (vacation.ToDate < x.FromDate)))
                       || (x.ToDate == null && vacation.ToDate == null && ((vacation.FromDate > x.FromDate) || (vacation.FromDate < x.FromDate))));

            return result;
        }

        private void CheckPositions(int contractorId, CustomContext context)
        {
            var check = _appDbContext.Positions.Any(x => x.ContractorId == contractorId);

            if (!check)
            {
                context.AddFail(ValidationCodes.POSITION_NOT_FOUND, ValidationMessages.InvalidReference);
                return;
            }
        }
    }
}
