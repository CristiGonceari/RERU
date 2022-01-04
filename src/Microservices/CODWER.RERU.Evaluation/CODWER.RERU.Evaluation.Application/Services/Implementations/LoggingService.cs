using CVU.ERP.Logging.Context;
using CVU.ERP.Module.Application.LoggerServices.Implementations;
using CVU.ERP.Module.Application.Providers;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class LoggingService<T> : LoggerService<T>, ILoggingService<T>
    {
        public sealed override string Project { get; protected set; }

        public LoggingService(LoggingDbContext localLoggingDbContext, IEnumerable<ICurrentApplicationUserProvider> userProvider) : base(localLoggingDbContext, userProvider)
        {
            Project = "Evaluation";
        }
    }
}
