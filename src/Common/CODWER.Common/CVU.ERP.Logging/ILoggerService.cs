using CVU.ERP.Logging.Models;
using System.Threading.Tasks;

namespace CVU.ERP.Logging
{
    public interface ILoggerService<T>
    {
        public Task Log(LogData data);
        public Task LogWithoutUser(LogData data);
    }
}
