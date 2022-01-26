using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;
using System;
using System.Linq;

namespace CODWER.RERU.Personal.Application.Vacations.AddContractorVacation
{
    public class AddVacationCommandValidator : AbstractValidator<AddVacationCommand>
    {
        private readonly AppDbContext _appDbContext;

        public AddVacationCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data.Mentions)
                     .NotEmpty()
                     .WithMessage(ValidationMessages.InvalidInput)
                     .WithErrorCode(ValidationCodes.INVALID_INPUT);

            When(x => x.Data.ToDate != null, () =>
            {
                RuleFor(x => x.Data)
                    .Must(x => x.ToDate > x.FromDate)
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationMessages.InvalidInput);
            });

            RuleFor(x => x.Data).Must(CheckVacationPeriod)
                .WithErrorCode(ValidationCodes.EXISTENT_RECORD)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Data)
                .Must(x => x.FromDate >= DateTime.Now.Date)
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.Data.VacationType)
                .IsInEnum()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            When(x => x.Data.VacationType == VacationType.Studies, () =>
            {
                RuleFor(x => x.Data.Institution)
                    .NotEmpty()
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT);
            });

            When(x => x.Data.VacationType == VacationType.ChildCare, () =>
            {
                RuleFor(x => x.Data.ChildAge)
                    .NotEmpty()
                    .Must(x => x > 0)
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT);
            });
        }
        private bool CheckVacationPeriod(AddEditVacationDto vacation)
        {

            var result = _appDbContext.Vacations
                .Where(x => x.ContractorId == vacation.ContractorId)
                .All (x => (x.ToDate != null && vacation.ToDate != null && (vacation.FromDate > x.ToDate && vacation.ToDate > x.ToDate) || (vacation.FromDate < x.FromDate && vacation.ToDate < x.FromDate))
                        || (x.ToDate != null && vacation.ToDate == null && ((vacation.FromDate > x.ToDate) || (vacation.FromDate < x.FromDate)))
                        || (x.ToDate == null && vacation.ToDate != null && ((vacation.FromDate > x.FromDate) || (vacation.ToDate < x.FromDate)))
                        || (x.ToDate == null && vacation.ToDate == null && ((vacation.FromDate > x.FromDate) || (vacation.FromDate < x.FromDate))));

            return result;
        }
    }
}
