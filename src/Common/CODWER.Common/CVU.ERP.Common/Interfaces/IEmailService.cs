using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CVU.ERP.Common.Interfaces
{
    public interface IEmailService
    {
        IEmailService From(string emailAddress);
        IEmailService AddRecipient(string emailAddress);
        IEmailService AddRecipients(IEnumerable<string> emailAddresses);

        IEmailService AddCCRecipient(string emailAddress);
        IEmailService AddCCRecipients(IEnumerable<string> emailAddresses);

        IEmailService AddSubject(string subject);
        IEmailService AddBody(string html);
        IEmailService AddAttachment(Stream content, string fileName, string mediaType, string mediaSubType);

        IEmailService Send();


        Task<IEmailService> QuickSendAsync(string subject, string body, string from, string to);
        Task<IEmailService> Send(string to, string subject, string body);
    }
}
