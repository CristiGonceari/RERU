using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

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
            var newtestTemplate = _mapper.Map<TestTemplate>(request.Data);

            _appDbContext.TestTemplates.Add(newtestTemplate);

            await _appDbContext.SaveChangesAsync();

            await LogAction(newtestTemplate);

            return newtestTemplate.Id;
        }

        private async Task LogAction(TestTemplate testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was created", testTemplate));
        }
    }
}
