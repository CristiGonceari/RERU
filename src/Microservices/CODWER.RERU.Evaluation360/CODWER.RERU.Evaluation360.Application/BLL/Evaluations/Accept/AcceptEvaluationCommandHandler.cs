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

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Accept
{
    public class AcceptEvaluationCommandHandler : IRequestHandler<AcceptEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly ISender _sender;

        public AcceptEvaluationCommandHandler(
            AppDbContext dbContext, 
            IMapper mapper, 
            INotificationService notificationService, 
            ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _sender = sender;
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
                await SendEmailNotification(evaluation.CounterSignerUserProfileId);
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

        private async Task<Unit> SendEmailNotification(int? counterSignerUserProfileId)
        {
            var counterSigner = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == counterSignerUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Revizuire Evaluare de performanță",
                To = counterSigner.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", counterSigner.FullName },
                    { "{email_message}", await GetTableContent() }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent()
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">a fost efectuata o evaluare, revizuiti evaluarea în rol de contrasemnatar.</p>";

            return content;
        }
    }
}