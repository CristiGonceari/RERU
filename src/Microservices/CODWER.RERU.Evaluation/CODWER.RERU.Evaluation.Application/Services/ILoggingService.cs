using CVU.ERP.Logging.Models;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface ILoggingService<T>
    {
        public Task Log(LogData data);

    }
}
