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

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignAccept
{
    public class CounterSignAcceptEvaluationCommandHandler : IRequestHandler<CounterSignAcceptEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public CounterSignAcceptEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(CounterSignAcceptEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            await SendEmailNotification(evaluation.EvaluatedUserProfileId);
            evaluation.Status = EvaluationStatusEnum.Contrasemnată;
            evaluation.DateCompletionCounterSigner = System.DateTime.Now;
            evaluation.SignatureCounterSigner = true;
            _mapper.Map(request.Evaluation, evaluation); 
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(int EvaluatedUserProfileId)
        {
            var user = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == EvaluatedUserProfileId);

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
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">evaluarea a fost acceptata de catre contrasemnatar, rugam sa luati cunostinta cu evaluarea</p>";

            return content;
        }
    }
}