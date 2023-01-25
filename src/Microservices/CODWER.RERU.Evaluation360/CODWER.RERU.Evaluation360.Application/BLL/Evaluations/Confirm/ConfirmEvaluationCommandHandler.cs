using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Common.DataTransferObjects.Config;
using RERU.Data.Entities.Evaluation360;
using System;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class ConfirmEvaluationCommandHandler : IRequestHandler<ConfirmEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly INotificationService _notificationService;
        private readonly PlatformConfig _platformConfig;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public ConfirmEvaluationCommandHandler(
            AppDbContext dbContext, 
            IMapper mapper, 
            ISender sender, 
            INotificationService notificationService,
            PlatformConfig platformConfig)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sender = sender;
            _notificationService = notificationService;
            _platformConfig = platformConfig;
        }

        public async Task<Unit> Handle(ConfirmEvaluationCommand request, CancellationToken cancellationToken)
        {
            await _sender.Send(new UpdateEvaluationCommand(request.Id, request.Evaluation));

            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            evaluation.Status = EvaluationStatusEnum.Confirmată;
            evaluation.DateCompletionGeneralData = System.DateTime.Now;
            evaluation.SignatureEvaluator = true;

            await _dbContext.SaveChangesAsync();
            await SendEmailNotification(evaluation);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(Evaluation evaluation)
        {
            var evaluated = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluation.EvaluatedUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Revizuire Evaluare de performanta",
                To = evaluated.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", evaluated.FullName },
                    { "{email_message}", await GetTableContent(evaluation.EvaluatorUserProfileId) }
                }
            });

            Console.WriteLine("-----------------------" + evaluated.Email);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(int evaluatorUserProfileId)
        {
            var evaluator = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatorUserProfileId);

            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">în cadrul evaluării de performanță, efectuată de {evaluator.FullName} 
                sunteți invitat/ă să reviziuiți evaluarea. </p><br>
                
                <p> Pentru a accesa evaluarea, urmați pașii: </p>
                <p> 1. Logați-vă pe pagina {_platformConfig.BaseUrl} </p>
                <p> 2. Accesați modulul ""Evaluare 360"" </p>
                <p> 3. Tastați ""Evaluări"" din meniul din stânga </p>
                <p> 4. Faceți click pe butonul ""Acceptă"", din coloana ""Acțiuni"" </p>
                <p> 5. Listați evaluarea până la final, completați rubrica obligatorie: ""Comentariile angajatului evaluat"" </p>
                <p> 6. Tastați ""Acceptă"" sau ""Respinge"" </p><br>
                
                <p> În cazul în care se alege ""Respinge"", evaluatorul va reevalua evaluarea și veți fi notificat pe email despre statutul evaluării. </p>";

            return content;
        }
    }
}