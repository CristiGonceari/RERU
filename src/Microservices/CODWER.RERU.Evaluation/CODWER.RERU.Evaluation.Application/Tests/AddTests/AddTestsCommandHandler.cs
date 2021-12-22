using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests
{
    public class AddTestsCommandHandler : IRequestHandler<AddTestsCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;

        public AddTestsCommandHandler(IMediator mediator, AppDbContext appDbContext)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(AddTestsCommand request, CancellationToken cancellationToken)
        {
            foreach (var testCommand in request.UserProfileId.Select(id => new AddTestCommand
            {
                Data = new AddEditTestDto
                {
                    UserProfileId = id,
                    EvaluatorId = request.EvaluatorId,
                    ShowUserName = request.ShowUserName,
                    TestTypeId = request.TestTypeId,
                    EventId = request.EventId,
                    LocationId = request.LocationId,
                    TestStatus = request.TestStatus,
                    ProgrammedTime = request.ProgrammedTime
                }
            }))
            {
                var testId = await _mediator.Send(testCommand);

                var generateCommand = new GenerateTestQuestionsCommand
                {
                    TestId = testId
                };

                await _mediator.Send(generateCommand);
            }

            return Unit.Value;
        }
    }
}
