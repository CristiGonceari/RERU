using FluentValidation;

namespace CODWER.RERU.Logging.Application.DeleteLoggingValues
{
    public class DeleteLoggingValuesCommandValidator : AbstractValidator<DeleteLoggingValuesCommand>
    {
        public DeleteLoggingValuesCommandValidator() 
        {
            RuleFor(x => x.PeriodOfYears)
                .GreaterThan(0);
        }
    }
}
