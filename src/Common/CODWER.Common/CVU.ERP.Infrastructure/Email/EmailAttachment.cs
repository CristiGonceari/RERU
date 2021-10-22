using System.IO;
using MimeKit;

namespace CVU.ERP.Infrastructure.Email
{
    public sealed class EmailAttachment
    {
        public Stream EmailContent { get; set; }

        public string FileName { get; set; }

        public ContentType ContentType { get; set; }
    }
}
