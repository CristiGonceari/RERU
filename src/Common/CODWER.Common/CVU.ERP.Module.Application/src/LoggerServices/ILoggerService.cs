using CVU.ERP.Logging.Models;
using System.Threading.Tasks;

namespace CVU.ERP.Module.Application.LoggerServices
{
    public interface ILoggerService<T>
    {
        public Task Log(LogData data);
    }
}
