using System;

namespace CODWER.RERU.Logging.DataTransferObjects
{
    public class LogDto
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string UserName { get; set; }
        public string UserIdentifier { get; set; }
        public string Event { get; set; }
        public string EventMessage { get; set; }
        public string JsonMessage { get; set; }
        public DateTime Date { get; set; }
    }
}
