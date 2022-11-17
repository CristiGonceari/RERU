﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities.Documents;

namespace CODWER.RERU.Personal.Application.DocumentTemplates.AddDocumentTemplate
{
    public class AddDocumentTemplateCommandHandler : IRequestHandler<AddDocumentTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddDocumentTemplateCommand> _loggerService;

        public AddDocumentTemplateCommandHandler(
            AppDbContext appDbContext, 
            IMapper mapper,
            ILoggerService<AddDocumentTemplateCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddDocumentTemplateCommand request, CancellationToken cancellationToken)
        {
            var mapperData = _mapper.Map<DocumentTemplate>(request.Data);

            await _appDbContext.DocumentTemplates.AddAsync(mapperData);
            await _appDbContext.SaveChangesAsync();

            await LogAction(mapperData);

            return mapperData.Id;
        }

        private async Task LogAction(DocumentTemplate documentTemplate)
        {
            await _loggerService.Log(LogData.AsPersonal($@"Șablonul de documente ""{documentTemplate.Name}"" a fost adăugat în sistem", documentTemplate));
        }
    }
}
