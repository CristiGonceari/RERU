using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionStatus
{
    public class EditQuestionStatusCommandHandler : IRequestHandler<EditQuestionStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<EditQuestionStatusCommandHandler> _loggerService;

        public EditQuestionStatusCommandHandler(AppDbContext appDbContext, ILoggerService<EditQuestionStatusCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(EditQuestionStatusCommand request, CancellationToken cancellationToken)
        {
            var updateQuestionStatus = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == request.Data.QuestionId);
            updateQuestionStatus.Status = request.Data.Status;

            await _appDbContext.SaveChangesAsync();

            await _loggerService.Log(new LogData
            {
                Project = "personal",
                EventMessage = "Was Edited Question Status Of The question"
            });

            return Unit.Value;
        }
    }
}
