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

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Reject
{
    public class RejectEvaluationCommandHandler : IRequestHandler<RejectEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public RejectEvaluationCommandHandler(
            AppDbContext dbContext, 
            IMapper mapper, 
            ISender sender, 
            INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<Unit> Handle(RejectEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            evaluation.Status = EvaluationStatusEnum.Respinsă;
            evaluation.DateAcceptOrRejectEvaluated = System.DateTime.Now;
            evaluation.SignatureEvaluated = true;

            _mapper.Map(request.Evaluation, evaluation); 

            await _dbContext.SaveChangesAsync();
            await SendEmailNotification(evaluation.EvaluatorUserProfileId, evaluation.EvaluatedUserProfileId);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(int evaluatorUserProfileId, int evaluatedUserProfileId)
        {
            var evaluator = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatorUserProfileId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Respingere de către evaluat a evaluării de performanță",
                To = evaluator.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", evaluator.FullName },
                    { "{email_message}", await GetTableContent(evaluatedUserProfileId) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(int evaluatedUserProfileId)
        {
            var evaluated = await _dbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatedUserProfileId);

            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">în cadrul evaluării de performanță, angajatul {evaluated.FullName} a respins evaluarea,
                sunteți invitat/ă să reevaluați angajatul. </p><br>";

            return content;
        }
    }
}