using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.AddTestTemplate
{
    public class AddTestTemplateCommandHandler : IRequestHandler<AddTestTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddTestTemplateCommandHandler> _loggerService;

        public AddTestTemplateCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<AddTestTemplateCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var newTestType = _mapper.Map<TestTemplate>(request.Data);

            _appDbContext.TestTemplates.Add(newTestType);

            await _appDbContext.SaveChangesAsync();

            await LogAction(newTestType);

            return newTestType.Id;
        }

        private async Task LogAction(Data.Entities.TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was created", testTemplate));
        }
    }
}
