using System.Threading.Tasks;
using CVU.ERP.Logging.Models;

namespace CVU.ERP.Module.Application.LoggerServices
{
    public interface ILoggerService<T>
    {
        public Task Log(LogData data);
    }
}
