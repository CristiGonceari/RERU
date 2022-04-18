using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.EditTestTemplate
{
    public class EditTestTemplateCommandHandler : IRequestHandler<EditTestTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditTestTemplateCommandHandler> _loggerService;

        public EditTestTemplateCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<EditTestTemplateCommandHandler> logger)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = logger;
        }

        public async Task<int> Handle(EditTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var updatetestTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, updatetestTemplate);
            await _appDbContext.SaveChangesAsync();

            await LogAction(updatetestTemplate);

            return updatetestTemplate.Id;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was edited", testTemplate));
        }
    }
}
