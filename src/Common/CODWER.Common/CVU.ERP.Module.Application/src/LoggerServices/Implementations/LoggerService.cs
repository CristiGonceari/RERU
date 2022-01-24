using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CVU.ERP.Module.Application.LoggerServices.Implementations
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly LoggingDbContext _localLoggingDbContext;
        private readonly IEnumerable<ICurrentApplicationUserProvider> _userProvider;
        public LoggerService(LoggingDbContext localLoggingDbContext, IEnumerable<ICurrentApplicationUserProvider> userProvider)
        {
            _localLoggingDbContext = localLoggingDbContext;
            _userProvider = userProvider;
        }

        public virtual async Task Log(LogData data)
        {
            await LocalLog(data);
        }

        private async Task LocalLog(LogData data)
        {
            var provider = _userProvider.FirstOrDefault(x => x.IsAuthenticated);

            if (provider == null)
            {
                throw new Exception("Null user provider service");
            }

            var coreUser = await provider.Get();

            if (coreUser == null)
            {
                throw new Exception("Null user logger service");
            }

            var toLog = new Log
            {
                Id = Guid.NewGuid().ToString(),
                Project = data.Project,
                UserName = coreUser.Name,
                UserIdentifier = coreUser.Id,
                Event = !string.IsNullOrWhiteSpace(data.Event) ? data.Event : ParseName(),
                EventMessage = data.EventMessage,
                Date = DateTime.Now,
                JsonMessage = data.SerializedObject
            };

            await _localLoggingDbContext.Logs.AddAsync(toLog);
            await _localLoggingDbContext.SaveChangesAsync();
        }

        private string ParseName()
        { 
            var splicedEventName = Regex.Split(typeof(T).Name, @"(?<!^)(?=[A-Z])").ToList();
            splicedEventName.Remove("Command");
            splicedEventName.Remove("Handler");
            splicedEventName.Remove("Query");

            return String.Join(" ", splicedEventName.ToArray());
        }
    }
}
