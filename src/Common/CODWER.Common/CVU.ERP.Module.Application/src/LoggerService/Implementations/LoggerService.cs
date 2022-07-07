﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CVU.ERP.Module.Application.LoggerService.Implementations
{
    public class LoggerService<T> : ILoggerService<T>
    {
        private readonly LoggingDbContext _localLoggingDbContext;
        private readonly IEnumerable<ICurrentApplicationUserProvider> _userProvider;
        public LoggerService(IEnumerable<ICurrentApplicationUserProvider> userProvider, IConfiguration configuration)
        {
            _localLoggingDbContext = LoggingDbContext.NewInstance(configuration);
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
                Event = !string.IsNullOrWhiteSpace(data.Event) ? data.Event : ParseName(),
                EventMessage = data.EventMessage,
                Date = DateTime.Now,
                JsonMessage = data.SerializedObject
            };

            await _localLoggingDbContext.Logs.AddAsync(toLog);
            await _localLoggingDbContext.SaveChangesAsync();

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
                UserName = $"{coreUser.LastName} {coreUser.FirstName} {coreUser.FatherName}",
                UserIdentifier = coreUser.Id,
                Event = !string.IsNullOrWhiteSpace(data.Event) ? data.Event : ParseName(),
                EventMessage = data.EventMessage,
                Date = DateTime.Now,
                JsonMessage = data.SerializedObject
            };

            await _localLoggingDbContext.Logs.AddAsync(toLog);
            await _localLoggingDbContext.SaveChangesAsync();

            ConsoleWrite(toLog);
        }

        private string ParseName()
        { 
            var splicedEventName = Regex.Split(typeof(T).Name, @"(?<!^)(?=[A-Z])").ToList();
            splicedEventName.Remove("Command");
            splicedEventName.Remove("Handler");
            splicedEventName.Remove("Query");

            return string.Join(" ", splicedEventName.ToArray());
        }

        private void ConsoleWrite(Log log)
        {
            var consoleMessage = JsonSerializer.Serialize(log, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            Console.WriteLine($"Logged message :\n {consoleMessage}\n JSON Entity :\n {log.JsonMessage}\n");
        }
    }
}