using System;
using System.Linq;
using System.Text.RegularExpressions;
using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails
{
    public class EditUserPersonalDetailsCommandValidator : AbstractValidator<EditUserPersonalDetailsCommand>
    {
        private readonly AppDbContext _appDbContext;

        public EditUserPersonalDetailsCommandValidator(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            RuleFor(x => x.Data.Id)
                .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(r => r.Data.FirstName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_USER_NAME);

            RuleFor(r => r.Data.LastName)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_USER_LAST_NAME);

            //RuleFor(r => r.Data.FatherName)
            //    .NotEmpty()
            //    .WithErrorCode(ValidationCodes.EMPTY_USER_FATHER_NAME);

            RuleFor(r => r.Data.PhoneNumber)
                .Must(x => Regex.IsMatch(x, @"^(\+373[0-9]{8})$"))
                .WithErrorCode(ValidationCodes.INVALID_USER_PHONE);

            RuleFor(r => r.Data.BirthDate)
                .Must(x => x < dateTime.Now.AddYears(-18))
                .WithErrorCode(ValidationCodes.INVALID_USER_BIRTH_DATE);

            RuleFor(x => x.Data.AccessModeEnum)
                .NotNull()
                .WithErrorCode(ValidationCodes.EMPTY_ACCESS_MODE);

            RuleFor(x => x.Data.Email).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_EMAIL)
                .EmailAddress()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_EMAIL_FORMAT);

            When(x => x.Data.DepartmentColaboratorId != null, () =>
            {
                RuleFor(x => x.Data.DepartmentColaboratorId.Value).Custom(CheckExistentDepartment);
            });

            When(x => x.Data.RoleColaboratorId != null, () =>
            {
                RuleFor(x => x.Data.RoleColaboratorId.Value).Custom(CheckExistentRole);
            });
        }

        private void CheckExistentDepartment(int colId, CustomContext context)
        {
            var exist = _appDbContext.Departments.Any(x => x.ColaboratorId == colId);

            if (!exist)
            {
                context.AddFail(ValidationCodes.INVALID_DEPARTMENT_ID, ValidationMessages.InvalidReference);
            }
        }


        private void CheckExistentRole(int roleId, CustomContext context)
        {
            var exist = _appDbContext.Roles.Any(x => x.ColaboratorId == roleId);

            if (!exist)
            {
                context.AddFail(ValidationCodes.INVALID_ROLE_ID, ValidationMessages.InvalidReference);
            }
        }
    }
}
