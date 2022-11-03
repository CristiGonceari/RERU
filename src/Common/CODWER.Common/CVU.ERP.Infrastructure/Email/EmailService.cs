using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace CVU.ERP.Infrastructure.Email
{
    public sealed class EmailService : IHideObjectMembers, IDisposable, IEmailService
    {
        private readonly string env;
        private readonly IEnumerable<MailboxAddress> systemErrorRecipients;

        private readonly SmtpOptions smtpOptions;

        private readonly ICollection<MailboxAddress> to;
        private readonly ICollection<MailboxAddress> cc;
        private readonly ICollection<MailboxAddress> bcc;
        private readonly ICollection<MimePart> attachments;

        private MessagePriority priority;

        private MailboxAddress from;
        private string subject;
        private MimePart body;

        public EmailService(IOptions<SmtpOptions> smtpOptions, IWebHostEnvironment env)
        {
            this.env = env.EnvironmentName;
            this.smtpOptions = smtpOptions.Value;
            systemErrorRecipients = ParseAddresses(this.smtpOptions.SystemErrorRecipients);

            from = new MailboxAddress(this.smtpOptions.FromName, this.smtpOptions.From);
            to = new List<MailboxAddress>();
            cc = new List<MailboxAddress>();
            bcc = new List<MailboxAddress>();
            attachments = new List<MimePart>();
            priority = MessagePriority.Normal;
        }

        /// <summary>
        /// Creates a new email instance using the default from
        /// address from smtp config settings
        /// </summary>
        public void FromDefault()
        {
            From(smtpOptions.From);
        }

        /// <summary>
        /// Creates a new Email instance and sets the from
        /// property
        /// </summary>
        /// <param name="emailAddress">Email address to send from</param>
        public IEmailService From(string emailAddress)
        {
            if (ValidateEmailAddress(emailAddress))
            {
                from = new MailboxAddress(smtpOptions.FromName, emailAddress);
            }

            return this;
        }

        #region To

        /// <summary>
        /// Adds a reciepient to the email
        /// </summary>
        /// <param name="emailAddress">Email address of recipeient</param>
        /// <returns></returns>
        public IEmailService AddRecipient(string emailAddress)
        {
            if (ValidateEmailAddress(emailAddress))
            {
                to.Add(new MailboxAddress(emailAddress));
            }

            return this;
        }

        /// <summary>
        /// Adds all reciepients in list to email
        /// </summary>
        /// <param name="mailAddresses">List of recipients</param>
        public IEmailService AddRecipients(IEnumerable<string> emailAddresses)
        {
            if (emailAddresses != null)
            {
                foreach (var address in emailAddresses)
                {
                    AddRecipient(address);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds system reciepients in list to email
        /// </summary>
        public IEmailService AddSystemEmails()
        {
            foreach (var address in systemErrorRecipients)
            {
                to.Add(address);
            }

            return this;
        }

        #endregion

        #region CC

        /// <summary>
        /// Adds a Carbon Copy to the email
        /// </summary>
        /// <param name="emailAddress">Email address to cc</param>
        public IEmailService AddCCRecipient(string email)
        {
            if(ValidateEmailAddress(email))
            {
                cc.Add(new MailboxAddress(email));
            }

            return this;
        }

        /// <summary>
        /// Adds all Carbon Copy in list to an email
        /// </summary>
        /// <param name="mailAddresses">List of recipients to CC</param>
        public IEmailService AddCCRecipients(IEnumerable<string> mailAddresses)
        {
            if (mailAddresses != null)
            {
                foreach (var address in mailAddresses)
                {
                    AddCCRecipient(address);
                }

            }

            return this;
        }

        #endregion

        /// <summary>
        /// Sets the subject of the email. If subject contains '\n', or '\r', or any combination of them, it will remove them 
        /// because they are invalid in email subject. 
        /// </summary>
        /// <param name="subjectParameter">email subject</param>
        public IEmailService AddSubject(string subjectParameter)
        {
            subject = subjectParameter.Replace("\r", string.Empty).Replace("\n", string.Empty);
            
            //subject = $"[{env}] [{AssemblyName()}] - {subject}";
            subject = $"[{smtpOptions.FromName}] - {subject} - Do Not Reply";

            return this;
        }

        /// <summary>
        /// Adds a Body to the Email
        /// </summary>
        /// <param name="bodyParameter">The content of the body</param>
        public IEmailService AddBody(string bodyParameter)
        {
            // var finalHtmlEmail = PreMailer.Net.PreMailer.MoveCssInline(body, true, null, null, true, true);
            body = new TextPart(TextFormat.Html)
            {
                Text = bodyParameter
            };

            return this;
        }

        #region Attachments
        /// <summary>
        /// Adds an Attachment to the Email
        /// </summary>
        /// <param name="attachment">The Attachment to add</param>
        public IEmailService AddAttachment(Stream content, string fileName, string mediaType, string mediaSubType)
        {
            var contentType = new ContentType(mediaType, mediaSubType);

            var att = new MimePart(contentType)
            {
                Content = new MimeContent(content),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = fileName
            };

            attachments.Add(att);

            return this;
        }
        #endregion

        /// <summary>
        /// Sends email synchronously
        /// </summary>
        public IEmailService Send()
        {
            // if (env == "Development")
            // {
            //     AddSystemEmails();
            // }

            using (var client = new SmtpClient())
            {
                if (env == "Development") client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect(smtpOptions.Host, smtpOptions.Port, smtpOptions.EnableSsl);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate(smtpOptions.UserName, smtpOptions.Password);

                client.Send(GenerateMessage());

                client.Disconnect(true);
            }

            return this;
        }


        /// <summary>
        /// Sends email asynchronously
        /// This is a quick send email, clears data from instance, adds needed data to instance, sends email and then clears all fields.
        /// </summary>
        public async Task<IEmailService> QuickSendAsync(string subject, string body, string from, string to)
        {
            this.to.Clear();
            cc.Clear();
            bcc.Clear();

            AddSubject(subject);
            AddBody(body);
            From(from);
            AddRecipient(to);

            if (env == "Development")
            {
                AddSystemEmails();
            }

            try
            {
                using (var client = new SmtpClient())
                {
                    Console.WriteLine("EMAIL: Email region");

                    if (env == "Development") client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (smtpOptions.IsOutlookEmail)
                    {
                        await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, SecureSocketOptions.StartTls);
                    }
                    else
                    {
                        await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, smtpOptions.EnableSsl);
                    }
                    await client.AuthenticateAsync(smtpOptions.UserName, smtpOptions.Password);
                    await client.SendAsync(GenerateMessage());
                    await client.DisconnectAsync(true);

                    Console.WriteLine("EMAIL: Disconnect client success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"EMAIL: ERROR send message to e ({to}), error ({e.Message})");
            }

            this.to.Clear();
            this.subject = "";
            this.body = null;

            return this;
        }

        public async Task<IEmailService> BulkSendAsync(List<EmailData> emailList)
        {
            this.to.Clear();
            cc.Clear();
            bcc.Clear();

            if (env == "Development")
            {
                AddSystemEmails();
            }

            try
            {
                using (var client = new SmtpClient())
                {
                    Console.WriteLine($"BULK-EMAIL: Email region {emailList.Count} to send");

                    if (env == "Development") client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (smtpOptions.IsOutlookEmail)
                    {
                        await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, SecureSocketOptions.StartTls);
                    }
                    else
                    {
                        await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, smtpOptions.EnableSsl);
                    }

                    await client.AuthenticateAsync(smtpOptions.UserName, smtpOptions.Password);

                    foreach(var email in emailList)
                    {
                        AddSubject(email.subject);
                        AddBody(email.body);
                        From(email.from);
                        AddRecipient(email.to);

                        try
                        {
                            await client.SendAsync(GenerateMessage());
                            Console.WriteLine($"BULK-EMAIL: Email success to ({email.to})");
                        }
                        catch (Exception x)
                        {
                            Console.WriteLine($"BULK-EMAIL: ERROR to ({x.Message})");
                        }
                    }

                    await client.DisconnectAsync(true);

                    Console.WriteLine("BULK-EMAIL: Disconnect client success");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"BULK-EMAIL: ERROR {e.Message}");
            }

            this.to.Clear();
            this.subject = "";
            this.body = null;

            return this;
        }

        public Task<IEmailService> Send(string toMail, string subjectMail, string bodyMail)
        {
            return QuickSendAsync(subjectMail, bodyMail, smtpOptions.From, toMail);
        }

        /// <summary>
        /// Releases all resources
        /// </summary>
        public void Dispose()
        {
        }

        private static IEnumerable<MailboxAddress> ParseAddresses(string emailAddresses)
        {
            if (string.IsNullOrEmpty(emailAddresses)) return Enumerable.Empty<MailboxAddress>();

            return emailAddresses.Split(';')
                .Select(x => x.Trim())
                .Where(ValidateEmailAddress)
                .Select(x => new MailboxAddress(x));
        }

        private static bool ValidateEmailAddress(string emailAddress)
        {
            return !string.IsNullOrEmpty(emailAddress) && new EmailAddressAttribute().IsValid(emailAddress);
        }

        private string AssemblyName()
        {
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
        }

        private MimeMessage GenerateMessage()
        {
            var message = new MimeMessage();
            message.From.Add(from);
            message.To.AddRange(to);
            message.Cc.AddRange(cc);
            message.Bcc.AddRange(bcc);
            message.ReplyTo.Add(from);
            message.Subject = subject;
            message.Priority = priority;

            var multipart = new Multipart("mixed")
                                {
                                    body
                                };
            foreach (var attachment in attachments) multipart.Add(attachment);

            message.Body = multipart;

            return message;
        }
    }
}