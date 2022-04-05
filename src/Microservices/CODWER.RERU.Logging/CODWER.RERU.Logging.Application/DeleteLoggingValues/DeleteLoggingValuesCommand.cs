using MediatR;

namespace CODWER.RERU.Logging.Application.DeleteLoggingValues
{
    public class DeleteLoggingValuesCommand : IRequest<Unit>
    {
       public int PeriodOfYears { get; set; }
    }
}
