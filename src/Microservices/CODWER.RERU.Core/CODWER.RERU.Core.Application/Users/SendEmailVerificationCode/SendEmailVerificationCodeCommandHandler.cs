using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.SendEmailVerificationCode
{
    public class SendEmailVerificationCodeCommandHandler : IRequestHandler<SendEmailVerificationCodeCommand, int>
    {
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly INotificationService _notificationService;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public SendEmailVerificationCodeCommandHandler(IPasswordGenerator passwordGenerator, INotificationService notificationService, AppDbContext appDbContext, IMapper mapper)
        {
            _passwordGenerator = passwordGenerator;
            _notificationService = notificationService;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(SendEmailVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            var code = _passwordGenerator.RandomEmailCode();

            var newEmailVerification = new EmailVerificationCodeDto()
            {
                Email = request.Email,
                Code = code
            };

            var emailVerificationDb = await _appDbContext.EmailVerifications.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (emailVerificationDb != null)
            {
                _mapper.Map(newEmailVerification, emailVerificationDb);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                var emailVerification = _mapper.Map<EmailVerification>(newEmailVerification);

                _appDbContext.EmailVerifications.Add(emailVerification);
                await _appDbContext.SaveChangesAsync();
            }

            //try
            //{
            //    var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
            //    var template = await File.ReadAllTextAsync(assemblyPath + "/EmailVerification.html");

            //    template = template.Replace("{Code}", code);

            //    var emailData = new EmailData()
            //    {
            //        subject = "Email verification",
            //        body = template,
            //        from = "Do Not Reply",
            //        to = request.Email
            //    };

            //    await _notificationService.Notify(emailData, NotificationType.Both);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine($"ERROR {e.Message}");
            //}

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Email verification",
                To = request.Email,
                HtmlTemplateAddress = "Templates/EmailVerification.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{Code}", code }
                }
            });

            return newEmailVerification.Id;
        }
    }
}
