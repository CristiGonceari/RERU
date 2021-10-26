using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Randoms.GetRandomNumber;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Core.Application.Users.ResetPasswordByEmail
{
    public class ResetPasswordByEmailCommandHandler : BaseHandler, IRequestHandler<ResetPasswordByEmailCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly ActiveTimeDto _activeTimeDto;

        public ResetPasswordByEmailCommandHandler(
            ICommonServiceProvider commonServiceProvider,
            IEmailService emailService,
            IOptions<ActiveTimeDto> activeTimeOption
        ) : base(commonServiceProvider)
        {
            _emailService = emailService;
            _activeTimeDto = activeTimeOption.Value;
        }

        public async Task<Unit> Handle(ResetPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            var userProf = await CoreDbContext.UserProfiles.FirstOrDefaultAsync(u => u.Email == request.Email);
            var now = DateTime.UtcNow;

            userProf.TokenLifetime = now.Add(_activeTimeDto.TokenLifetime);
            userProf.Token = await Mediator.Send(new GetRandomNumberQuery()); ;

            await CoreDbContext.SaveChangesAsync();

            string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Templates";
            var template = File.ReadAllText(assemblyPath + "/ResetByEmailToken.html");

            template = template
                .Replace("{FirstName}", userProf.Name + " " + userProf.LastName)
                .Replace("{Token}", userProf.Token);

            try
            {
                await _emailService.QuickSendAsync(subject: "Reset Password",
                    body: template,
                    from: "Do Not Reply",
                    to: userProf.Email);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR {e.Message}");
            }

            return Unit.Value;
        }
    }
}