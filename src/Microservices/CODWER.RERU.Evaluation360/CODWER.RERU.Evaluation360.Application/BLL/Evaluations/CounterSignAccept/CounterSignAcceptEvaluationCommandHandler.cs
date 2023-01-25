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

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignAccept
{
    public class CounterSignAcceptEvaluationCommandHandler : IRequestHandler<CounterSignAcceptEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public CounterSignAcceptEvaluationCommandHandler(
            AppDbContext dbContext, 
            IMapper mapper, 
            ISender sender, 
            INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(CounterSignAcceptEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            evaluation.Status = EvaluationStatusEnum.Contrasemnată;
            evaluation.DateCompletionCounterSigner = System.DateTime.Now;
            evaluation.SignatureCounterSigner = true;

            _mapper.Map(request.Evaluation, evaluation);

            await _dbContext.SaveChangesAsync();
            await SendEmailNotification(evaluation.EvaluatedUserProfileId);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(int evaluatedUserProfileId)
        {
            var evaluated = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatedUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Revizuire Evaluare de performanță",
                To = evaluated.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", evaluated.FullName },
                    { "{email_message}", await GetTableContent() }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent()
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;""> în cadrul evaluării de performanță, evaluarea a fost acceptată de către contrasemnatar, rugam să luați cunostință cu rezultatul final. </p>";

            return content;
        }
    }
}