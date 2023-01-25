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
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.EvaluatedKnow;
using RERU.Data.Entities.Evaluation360;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Common.DataTransferObjects.Response;
using Newtonsoft.Json.Linq;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Accept
{
    public class AcceptEvaluationCommandHandler : IRequestHandler<AcceptEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ISender _sender;
        private readonly PlatformConfig _platformConfig;

        public AcceptEvaluationCommandHandler(
            AppDbContext dbContext, 
            IMapper mapper, 
            INotificationService notificationService, 
            ISender sender,
            PlatformConfig platformConfig)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _sender = sender;
            _platformConfig = platformConfig;
        }

        public async Task<Unit> Handle(AcceptEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            if (evaluation.CounterSignerUserProfileId != null)
            {
                evaluation.Status = EvaluationStatusEnum.Acceptată;
                evaluation.DateAcceptOrRejectEvaluated = System.DateTime.Now;
                evaluation.SignatureEvaluated = true;

                _mapper.Map(request.Evaluation, evaluation);

                await _dbContext.SaveChangesAsync();
                await SendEmailNotification(evaluation);
            }
            else
            {
                evaluation.Status = EvaluationStatusEnum.Acceptată;
                evaluation.DateAcceptOrRejectEvaluated = System.DateTime.Now;
                evaluation.SignatureEvaluated = true;

                _mapper.Map(request.Evaluation, evaluation);

                await _dbContext.SaveChangesAsync();
                await _sender.Send(new EvaluatedKnowCommand(request.Id));
            }

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(Evaluation evaluation)
        {
            var counterSigner = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluation.CounterSignerUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Acceptare Evaluare de performanță",
                To = counterSigner.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", counterSigner.FullName },
                    { "{email_message}", await GetTableContent(evaluation.EvaluatorUserProfileId, evaluation.EvaluatedUserProfileId) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(int evaluatorUserProfileId, int evaluatedUserProfileId)
        {            
            var evaluator = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatorUserProfileId);
            var evaluated = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatedUserProfileId);

            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">în cadrul evaluării de performanță, a angajatului {evaluated.FullName},  efectuată de {evaluator.FullName} 
                sunteți invitat/ă să contrasemnați evaluarea. </p><br>

                < p > În cazul în care se alege ""Respinge"", evaluatorul va re - evalua evaluarea și veți fi notificat pe email despre statutul evaluării. </ p > ";

            return content;
        }
    }
}