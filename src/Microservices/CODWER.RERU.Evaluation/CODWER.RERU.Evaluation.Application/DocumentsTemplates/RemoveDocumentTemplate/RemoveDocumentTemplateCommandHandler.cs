﻿using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Documents;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.DocumentsTemplates.RemoveDocumentTemplate
{
    public class RemoveDocumentTemplateCommandHandler : IRequestHandler<RemoveDocumentTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<RemoveDocumentTemplateCommand> _loggerService;

        public RemoveDocumentTemplateCommandHandler(AppDbContext appDbContext, ILoggerService<RemoveDocumentTemplateCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(RemoveDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.DocumentTemplates.FirstAsync(d => d.Id == request.Id);

            _appDbContext.DocumentTemplates.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            await LogAction(toRemove);

            return Unit.Value;
        }

        private async Task LogAction(DocumentTemplate documentTemplate)
        {
            await _loggerService.Log(LogData.AsPersonal($"{documentTemplate.Name} was removed from document templates list", documentTemplate));
        }
    }
}