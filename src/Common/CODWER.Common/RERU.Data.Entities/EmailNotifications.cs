using System.Collections.Generic;

namespace RERU.Data.Entities
{
    public class EmailNotification
    {
        public EmailNotification()
        {
            Properties = new HashSet<EmailNotificationProperty>();
        }
        public int Id { get; set; }

        public string Subject { get; set; }
        public string To { get; set; }

        public bool IsSend { get; set; }
        public bool InUpdateProcess { get; set; }

        public string HtmlTemplateAddress { get; set; }
        public string Status { get; set; }

        public byte Type { get; set; } // 0 - Local, 1 - MNotify, 2 - Both

        public virtual ICollection<EmailNotificationProperty> Properties { get; set; }
    }
}
