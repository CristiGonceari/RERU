using System;
using System.Text.Json.Serialization;

namespace CVU.ERP.Logging.Entities
{
    public class Log
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string UserName { get; set; }
        public string UserIdentifier { get; set; }
        public string Event { get; set; }
        public string EventMessage { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public string JsonMessage { get; set; }
    }
}
