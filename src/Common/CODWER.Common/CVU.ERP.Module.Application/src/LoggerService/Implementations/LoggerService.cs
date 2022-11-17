using CVU.ERP.Logging;
using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using CVU.ERP.Logging.Models;
using CVU.ERP.ServiceProvider;
using RERU.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider.Models;

namespace CVU.ERP.Module.Application.LoggerService.Implementations
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly LoggingDbContext _localLoggingDbContext;
        private readonly IEnumerable<ICurrentApplicationUserProvider> _userProvider;
        public LoggerService(IEnumerable<ICurrentApplicationUserProvider> userProvider, LoggingDbContext loggingDbContext)
        {
            _localLoggingDbContext = loggingDbContext;
            _userProvider = userProvider;
        }

        public virtual async Task Log(LogData data)
        {
            await LocalLog(data);
        }

        public async Task LogWithoutUser(LogData data)
        {
            var user = JsonSerializer.Deserialize<UserProfile>(data.SerializedObject.ToString());
            var toLog = new Log
            {
                Id = Guid.NewGuid().ToString(),
                Project = data.Project,
                UserName = $"{user.LastName} {user.FirstName} {user.FatherName}",
                UserIdentifier = "",
                Event = !string.IsNullOrWhiteSpace(data.Event) ? data.Event : ParseEvent(),
                EventMessage = data.EventMessage,
                Date = DateTime.Now,
                JsonMessage = data.SerializedObject
            };

            await using var db = _localLoggingDbContext.NewInstance();
            await db.Logs.AddAsync(toLog);
            await db.SaveChangesAsync();

            ConsoleWrite(toLog);
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
                UserName = $"{coreUser.FullName}",
                UserIdentifier = coreUser.Id,
                Event = !string.IsNullOrWhiteSpace(data.Event) ? data.Event : ParseEvent(),
                EventMessage = ParseEventMessage(data.EventMessage, coreUser),
                Date = DateTime.Now,
                JsonMessage = data.SerializedObject
            };

            await using var db = _localLoggingDbContext.NewInstance();
            await db.Logs.AddAsync(toLog);
            await db.SaveChangesAsync();

            ConsoleWrite(toLog);
        }

        private string ParseEvent()
        { 
            var splicedEventName = Regex.Split(typeof(T).Name, @"(?<!^)(?=[A-Z])").ToList();
            splicedEventName.Remove("Command");
            splicedEventName.Remove("Handler");
            splicedEventName.Remove("Query");

            var splitString = string.Join("", splicedEventName.ToArray());

            return ParseEventName(splitString);
        }

        private void ConsoleWrite(Log log)
        {
            var consoleMessage = JsonSerializer.Serialize(log, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            Console.WriteLine($"Logged message :\n {consoleMessage}\n JSON Entity :\n {log.JsonMessage}\n");
        }

        private string ParseEventName(string fieldName)
        {
            var objType = typeof(ActionEventNames);

            FieldInfo[] fields = objType.GetFields(BindingFlags.Static | BindingFlags.Public);

            var fieldInfo = fields.FirstOrDefault(x => x.Name.Contains(fieldName));

            return fieldInfo != null ? fieldInfo.GetValue(objType)?.ToString() : string.Empty;
        }

        private string ParseEventMessage(string eventMessage, ApplicationUser coreUser)
        {
            return eventMessage + $@", de către ""{coreUser.FullName}""";
        }
    }
}
