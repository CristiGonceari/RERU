using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.UpdateOrganizationalChart
{
    public class UpdateOrganizationalChartCommandValidator : AbstractValidator<UpdateOrganizationalChartCommand>
    {
        public UpdateOrganizationalChartCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<OrganizationalChart>(appDbContext, ValidationCodes.ORGANIZATIONAL_CHART_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.Data).SetValidator(new OrganizationalChartValidator(appDbContext));
        }
    }
}
