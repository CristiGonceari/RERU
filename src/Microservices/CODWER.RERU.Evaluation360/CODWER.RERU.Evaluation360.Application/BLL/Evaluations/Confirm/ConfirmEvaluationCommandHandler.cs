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
using RERU.Data.Entities;
using System.Linq;
using System;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class ConfirmEvaluationCommandHandler : IRequestHandler<ConfirmEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public ConfirmEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender, IInternalNotificationService internalNotificationService, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sender = sender;
            _internalNotificationService = internalNotificationService;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(ConfirmEvaluationCommand request, CancellationToken cancellationToken)
        {
            await _sender.Send(new UpdateEvaluationCommand(request.Id, request.Evaluation));
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            await _internalNotificationService.AddNotification(evaluation.EvaluatedUserProfileId,"");
            await SendEmailNotification(evaluation.EvaluatedUserProfileId);
            evaluation.Status = EvaluationStatusEnum.Confirmed;
            evaluation.DateCompletionGeneralData = System.DateTime.Now;
            evaluation.SignatureEvaluator = true;
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(int EvaluatedUserProfileId)
        {
            var user = await _dbContext.UserProfiles
                .Include(e=> e.Email)
                .FirstOrDefaultAsync(x => x.Id == EvaluatedUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.Email },
                    { "{email_message}", await GetTableContent() }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent()
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">ati fost evaluat/ă, revizuiti evaluarea în rol de angajat.</p>";

            return content;
        }
    }
}