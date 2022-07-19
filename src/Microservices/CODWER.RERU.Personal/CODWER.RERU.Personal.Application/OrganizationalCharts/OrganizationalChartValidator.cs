using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts
{
    public class OrganizationalChartValidator : AbstractValidator<AddEditOrganizationalChartDto>
    {
        public OrganizationalChartValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
