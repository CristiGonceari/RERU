using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.Validators.DepartmentRoleContent
{
    public class ExistentDepartmentRoleContentRecordValidator : AbstractValidator<AddEditDepartmentRoleContentDto>
    {
        private readonly AppDbContext _appDbContext;

        public ExistentDepartmentRoleContentRecordValidator(AppDbContext appDbContext, string errorMessage)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom((data, c) => ExistentRecord(data, errorMessage, c));
        }

        private void ExistentRecord(AddEditDepartmentRoleContentDto data, string errorMessage, CustomContext context)
        {
            var existent = _appDbContext.DepartmentRoleContents.Any(dr =>
                dr.DepartmentId == data.DepartmentId && dr.RoleId == data.OrganizationRoleId);

            if (existent)
            {
                context.AddFail(ValidationCodes.EXISTENT_RECORD, errorMessage);
            }
        }
    }
}
