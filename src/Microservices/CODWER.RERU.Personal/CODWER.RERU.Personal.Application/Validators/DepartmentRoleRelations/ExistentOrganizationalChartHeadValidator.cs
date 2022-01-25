using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;

namespace CODWER.RERU.Personal.Application.Validators.DepartmentRoleRelations
{
    public class ExistentOrganizationalChartHeadValidator : AbstractValidator<int>
    {
        private readonly AppDbContext _appDbContext;

        public ExistentOrganizationalChartHeadValidator(AppDbContext appDbContext, string errorMessage)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom((id, c) => ExistentHead(id, errorMessage, c));
        }

        private void ExistentHead(int id, string errorMessage, CustomContext c)
        {
            var headDb = _appDbContext.DepartmentRoleRelations.FirstOrDefault(x =>
                x.OrganizationalChartId == id
                && ((x is ParentDepartmentChildDepartment) && ((ParentDepartmentChildDepartment)x).ParentDepartmentId == null
                    || (x is ParentDepartmentChildOrganizationRole) && ((ParentDepartmentChildOrganizationRole)x).ParentDepartmentId == null));

            if (headDb != null)
            {
                c.AddFail(ValidationCodes.EXISTENT_RECORD, errorMessage);
            }
        }
    }
}
