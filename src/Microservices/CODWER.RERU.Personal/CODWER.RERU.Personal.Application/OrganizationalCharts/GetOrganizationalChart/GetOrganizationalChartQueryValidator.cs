using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalChart
{
    public class GetOrganizationalChartQueryValidator : AbstractValidator<GetOrganizationalChartQuery>
    {
        public GetOrganizationalChartQueryValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<OrganizationalChart>(appDbContext, ValidationCodes.ORGANIZATIONAL_CHART_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
