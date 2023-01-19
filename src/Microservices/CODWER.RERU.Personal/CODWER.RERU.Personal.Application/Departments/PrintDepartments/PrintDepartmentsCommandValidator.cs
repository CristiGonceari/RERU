using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;
using System.Linq;

namespace CODWER.RERU.Personal.Application.Departments.PrintDepartments
{
    public class PrintDepartmentsCommandValidator : AbstractValidator<PrintDepartmentsCommand>
    {
        public PrintDepartmentsCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<DepartmentDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }

    }
}
