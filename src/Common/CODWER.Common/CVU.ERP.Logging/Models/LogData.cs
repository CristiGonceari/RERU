namespace CVU.ERP.Logging.Models
{
    public class LogData
    {
        public LogData(string message)
        {
            EventMessage = message;
        }

        public string Project { get; set; }
        public string Event { get; set; }
        public string EventMessage { get; set; }

        public LogData AsEvaluation()
        {
            Project = Projects.EVALUATION;

            return this;
        }

        public LogData AsPersonal()
        {
            Project = Projects.PERSONAL;

            return this;
        }

        public LogData AsCore()
        {
            Project = Projects.CORE;

            return this;
        }

    }
}
