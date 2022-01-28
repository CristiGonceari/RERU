using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.EditTestType
{
    public class EditTestTypeCommandHandler : IRequestHandler<EditTestTypeCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditTestTypeCommandHandler> _loggerService;

        public EditTestTypeCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<EditTestTypeCommandHandler> logger)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = logger;
        }

        public async Task<int> Handle(EditTestTypeCommand request, CancellationToken cancellationToken)
        {
            var updateTestType = await _appDbContext.TestTypes.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, updateTestType);
            await _appDbContext.SaveChangesAsync();

            await LogAction(updateTestType);

            return updateTestType.Id;
        }

        private async Task LogAction(TestType testTemplate)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Test template was edited", testTemplate));
        }
    }
}
