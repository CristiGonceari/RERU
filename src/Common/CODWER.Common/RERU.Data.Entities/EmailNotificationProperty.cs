namespace RERU.Data.Entities
{
    public class EmailNotificationProperty
    {
        public int Id { get; set; }

        public string KeyToReplace { get; set; }
        public string ValueToReplace { get; set; }

        public int EmailNotificationId { get; set; }
        public EmailNotification EmailNotification { get; set; }
    }
}
