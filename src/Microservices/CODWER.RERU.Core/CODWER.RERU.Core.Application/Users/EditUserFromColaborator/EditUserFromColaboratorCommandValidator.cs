using System;
using System.Linq;
using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditUserFromColaborator
{
    public class EditUserFromColaboratorCommandValidator : AbstractValidator<EditUserFromColaboratorCommand>
    {
        private readonly AppDbContext _appDbContext;
        public EditUserFromColaboratorCommandValidator(IServiceProvider serviceProvider)
        {
            _appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_NAME);

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_LAST_NAME);

            RuleFor(x => x.FatherName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_FATHER_NAME);

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_EMAIL)
                .EmailAddress()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_EMAIL_FORMAT);

            RuleFor(x => x.AccessModeEnum)
                .NotNull()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_ACCESS_MODE);

            When(r => r.DepartmentColaboratorId != null, () =>
            {
                RuleFor(x => x.DepartmentColaboratorId.Value).Custom(CheckExistentDepartment);
            });

            When(r => r.RoleColaboratorId != null, () =>
            {
                RuleFor(x => x.RoleColaboratorId.Value).Custom(CheckExistentRole);
            });

            RuleFor(x => x)
              .Custom(CheckIfUniqueIdnpOnCreate);
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

        private void CheckIfUniqueIdnpOnCreate(EditUserFromColaboratorCommand data, CustomContext context)
        {
            var exist = _appDbContext.UserProfiles.Any(x =>x.Id != data.Id && x.Idnp == data.Idnp);

            if (exist)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}
