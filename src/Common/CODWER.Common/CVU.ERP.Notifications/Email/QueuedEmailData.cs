using System.Collections.Generic;

namespace CVU.ERP.Notifications.Email
{
    public class QueuedEmailData
    {
        public string Subject { get; set; }
        public string To { get; set; }

        public Dictionary<string,string> ReplacedValues { get; set; }
        public string HtmlTemplateAddress { get; set; }
    }
}