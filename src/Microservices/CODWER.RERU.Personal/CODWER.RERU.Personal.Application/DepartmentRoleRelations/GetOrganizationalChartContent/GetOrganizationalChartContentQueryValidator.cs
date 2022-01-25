using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetOrganizationalChartContent
{
    public class GetOrganizationalChartContentQueryValidator : AbstractValidator<GetOrganizationalChartContentQuery>
    {
        public GetOrganizationalChartContentQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.OrganizationalChartId)
                .SetValidator(new ItemMustExistValidator<OrganizationalChart>(appDbContext, ValidationCodes.ORGANIZATIONAL_CHART_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
