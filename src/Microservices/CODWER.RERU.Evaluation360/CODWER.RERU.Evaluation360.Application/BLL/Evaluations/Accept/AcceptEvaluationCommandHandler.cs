using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Evaluation360.Application.BLL.Services;
using System.Collections.Generic;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Accept
{
    public class AcceptEvaluationCommandHandler : IRequestHandler<AcceptEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AcceptEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(AcceptEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            if (evaluation.CounterSignerUserProfileId != null)
            {
                await SendEmailNotification(evaluation.CounterSignerUserProfileId);
                evaluation.Status = EvaluationStatusEnum.Acceptată;
                evaluation.DateAcceptOrRejectEvaluated = System.DateTime.Now;
                evaluation.SignatureEvaluated = true;
                _mapper.Map(request.Evaluation, evaluation);
                await _dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(int? CounterSignerUserProfileId)
        {
            var user = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == CounterSignerUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FirstName + " " + user.LastName + " " + user.FatherName },
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