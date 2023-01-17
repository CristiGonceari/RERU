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

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignReject
{
    public class CounterSignRejectEvaluationCommandHandler : IRequestHandler<CounterSignRejectEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public CounterSignRejectEvaluationCommandHandler(
            AppDbContext dbContext, 
            IMapper mapper, 
            ISender sender, 
            INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(CounterSignRejectEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            evaluation.Status = EvaluationStatusEnum.Respinsă_contrasemnatar;
            evaluation.DateCompletionCounterSigner = System.DateTime.Now;
            evaluation.SignatureCounterSigner = true;

            _mapper.Map(request.Evaluation, evaluation); 

            await _dbContext.SaveChangesAsync();
            await SendEmailNotificationToEvaluator(evaluation.EvaluatorUserProfileId);
            await SendEmailNotificationToEvaluated(evaluation.EvaluatedUserProfileId);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotificationToEvaluator(int evaluatorUserProfileId)
        {
            var evaluator = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatorUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Revizuire Evaluare de performanță",
                To = evaluator.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", evaluator.FullName },
                    { "{email_message}", await GetEvaluatorTableContent() }
                }
            });

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotificationToEvaluated(int evaluatedUserProfileId)
        {
            var evaluated = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatedUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Respingere de către contrasemnatar a evaluării de performanță",
                To = evaluated.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", evaluated.FullName },
                    { "{email_message}", await GetEvaluatedTableContent() }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetEvaluatorTableContent()
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">evaluarea a fost respinsa de catre contrasemnatar, rugam sa reevaluati angajatul.</p>";

            return content;
        }

        private async Task<string> GetEvaluatedTableContent()
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">evaluarea a fost respinsa de catre contrasemnatar, urmează ca evaluatorul să facă reevaluarea și să fiți notificat pe email despre statutul evaluării.</p>";

            return content;
        }
    }
}