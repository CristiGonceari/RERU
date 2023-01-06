using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplateStatus
{
    public class EditTestTemplateStatusCommandHandler : IRequestHandler<EditTestTemplateStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<EditTestTemplateStatusCommandHandler> _loggerService;

        public EditTestTemplateStatusCommandHandler(AppDbContext appDbContext, ILoggerService<EditTestTemplateStatusCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(EditTestTemplateStatusCommand request, CancellationToken cancellationToken)
        {
            var updatetestTemplate = await _appDbContext.TestTemplates.FirstAsync(x => x.Id == request.Data.TestTemplateId);
            updatetestTemplate.Status = request.Data.Status;

            await _appDbContext.SaveChangesAsync();

            await LogAction(updatetestTemplate);

            return Unit.Value;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($@"Statutul șablonului de test ""{testTemplate.Name}"" a primit un nou statut: ""{await ParseStatus(testTemplate.Status)}""", testTemplate));
        }

        private async Task<string> ParseStatus(TestTemplateStatusEnum status) => 
            status switch
            {
                TestTemplateStatusEnum.Active => "activ",
                TestTemplateStatusEnum.Canceled => "anulat",
                TestTemplateStatusEnum.Draft => "maculator",
                _ => string.Empty
            };
        

    }
}
