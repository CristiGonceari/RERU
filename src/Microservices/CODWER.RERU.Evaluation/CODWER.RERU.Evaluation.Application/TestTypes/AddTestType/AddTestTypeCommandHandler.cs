using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddTestType
{
    public class AddTestTypeCommandHandler : IRequestHandler<AddTestTypeCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddTestTypeCommandHandler> _loggerService;

        public AddTestTypeCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<AddTestTypeCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddTestTypeCommand request, CancellationToken cancellationToken)
        {
            var newTestType = _mapper.Map<TestType>(request.Data);

            _appDbContext.TestTypes.Add(newTestType);

            await _appDbContext.SaveChangesAsync();

            await LogAction(newTestType);

            return newTestType.Id;
        }

        private async Task LogAction(TestType testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was created", testTemplate));
        }
    }
}
