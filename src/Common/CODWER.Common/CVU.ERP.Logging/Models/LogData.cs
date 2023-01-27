using System.Text.Json;
using System.Text.Json.Serialization;

namespace CVU.ERP.Logging.Models
{
    public class LogData
    {
        public LogData(string message, string project, string obj = "")
        {
            EventMessage = message;
            Project = project;
            SerializedObject = obj;
        }

        public string Project { get; set; }
        public string Event { get; set; }
        public string EventMessage { get; set; }
        public string SerializedObject { get; set; }

        public static LogData AsEvaluation360(string message) => new (message, Projects.EVALUATION360);
        public static LogData AsEvaluation(string message) => new (message, Projects.EVALUATION);
        public static LogData AsEvaluation<T>(string message, T obj) => new (message, Projects.EVALUATION, Serialize(obj));

        public static LogData AsPersonal(string message) => new(message, Projects.PERSONAL);
        public static LogData AsPersonal<T>(string message, T obj) => new(message, Projects.PERSONAL, Serialize(obj));

        public static LogData AsCore(string message) => new(message, Projects.CORE);
        public static LogData AsCore<T>(string message, T obj) => new(message, Projects.CORE, Serialize(obj));

        private static string Serialize<T>(T obj) => 
            obj != null 
                ? JsonSerializer.Serialize(obj,new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                }) 
                : string.Empty;
    }
}
