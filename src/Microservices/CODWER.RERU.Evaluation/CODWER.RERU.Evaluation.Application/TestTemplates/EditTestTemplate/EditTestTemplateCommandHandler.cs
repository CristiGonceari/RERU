using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var updateTestType = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, updateTestType);
            await _appDbContext.SaveChangesAsync();

            await LogAction(updateTestType);

            return updateTestType.Id;
        }

        private async Task LogAction(Data.Entities.TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was edited", testTemplate));
        }
    }
}
