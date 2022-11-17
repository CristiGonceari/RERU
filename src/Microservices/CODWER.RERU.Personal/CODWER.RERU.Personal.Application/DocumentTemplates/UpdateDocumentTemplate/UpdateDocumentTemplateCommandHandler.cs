﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.UpdateDocumentTemplate
{
    public class UpdateDocumentTemplateCommandHandler : IRequestHandler<UpdateDocumentTemplateCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<UpdateDocumentTemplateCommand> _loggerService;

        public UpdateDocumentTemplateCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper,
            ILoggerService<UpdateDocumentTemplateCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(UpdateDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.HrDocumentTemplates.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return Unit.Value;
        }
        private async Task LogAction(HrDocumentTemplate documentTemplate)
        {
            await _loggerService.Log(LogData.AsPersonal($"Șablonul de documente {documentTemplate.Name} a fost actualizat", documentTemplate));
        }
    }
}
