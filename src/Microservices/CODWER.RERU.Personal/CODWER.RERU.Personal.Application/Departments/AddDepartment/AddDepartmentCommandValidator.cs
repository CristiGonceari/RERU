using FluentValidation;

namespace CODWER.RERU.Personal.Application.Departments.AddDepartment
{
    public class AddDepartmentCommandValidator : AbstractValidator<AddDepartmentCommand>
    {
        public AddDepartmentCommandValidator()
        {
            RuleFor(x => x.Data).SetValidator(new DepartmentValidator());
        }
    }
}
