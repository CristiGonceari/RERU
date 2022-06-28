using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Notifications.Services;
using MediatR;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddEvaluations
{
    public class AddEvaluationsCommandHandler : IRequestHandler<AddEvaluationsCommand, List<int>>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;

        public AddEvaluationsCommandHandler(IMediator mediator, AppDbContext appDbContext, INotificationService notificationService)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _notificationService = notificationService;
        }

        public async Task<List<int>> Handle(AddEvaluationsCommand request, CancellationToken cancellationToken)
        {
            int testId = 0;
            var testsIds = new List<int>();

            for (int i = 0; i < request.EvaluatorId.Count; i++)
            {
                for (int j = 0; j < request.UserProfileId.Count; j++)
                {
                    var addCommand = new AddTestCommand
                    {
                        Data = new AddEditTestDto
                        {
                            UserProfileId = request.UserProfileId[j],
                            EvaluatorId = request.EvaluatorId[i],
                            ShowUserName = request.ShowUserName,
                            TestTemplateId = request.TestTemplateId,
                            EventId = request.EventId,
                            TestStatus = request.TestStatus,
                            ProgrammedTime = request.ProgrammedTime
                        }
                    };

                    testId = await _mediator.Send(addCommand);

                    testsIds.Add(testId);

                    var generateCommand = new GenerateTestQuestionsCommand
                    {
                        TestId = testId
                    };

                    await _mediator.Send(generateCommand);
                    //await SendEmailNotification(addCommand, null, testId);
                }
            }

            //await SendEmailNotification(null, request, testId);

            return testsIds;
        }
    }
}
