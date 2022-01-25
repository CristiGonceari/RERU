using System;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Positions
{
    public class PositionValidator : AbstractValidator<AddEditPositionDto>
    {
        public PositionValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.FromDate)
                .NotEmpty()
                .Must(x=>x > DateTime.Now.AddYears(-100))
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.GeneratedDate)
                .NotEmpty()
                .Must(x => x > DateTime.Now.AddYears(-100))
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.ProbationDayPeriod).Must(x => x >= 0)
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => (int)x.WorkHours)
                .SetValidator(new ExistInEnumValidator<WorkHoursEnum>());

            RuleFor(x => x.OrganizationRoleId)
                .SetValidator(new ItemMustExistValidator<OrganizationRole>(appDbContext, ValidationCodes.ORGANIZATION_ROLE_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.DepartmentId)
                .SetValidator(new ItemMustExistValidator<Department>(appDbContext, ValidationCodes.DEPARTMENT_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
