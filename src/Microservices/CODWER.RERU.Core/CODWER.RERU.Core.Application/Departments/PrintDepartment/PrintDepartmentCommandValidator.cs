using System.Linq;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CVU.ERP.Common.Validation;
using CVU.ERP.Module.Application.TableExportServices.Validators;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Departments.PrintDepartment
{
    public class PrintDepartmentCommandValidator : AbstractValidator<PrintDepartmentCommand>
    {
        public PrintDepartmentCommandValidator()
        {
            RuleFor(x => x.Fields.Select(x => x.Value).ToList())
                .SetValidator(new TableExporterValidator<DepartmentDto>(ValidationMessages.InvalidInput, ValidationCodes.INVALID_INPUT));
        }
    }
}
