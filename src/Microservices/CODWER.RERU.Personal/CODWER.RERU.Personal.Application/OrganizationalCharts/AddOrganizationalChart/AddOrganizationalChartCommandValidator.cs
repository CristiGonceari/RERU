using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart
{
    public class AddOrganizationalChartCommandValidator : AbstractValidator<AddOrganizationalChartCommand>
    {
        public AddOrganizationalChartCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new OrganizationalChartValidator(appDbContext));
        }
    }
}
