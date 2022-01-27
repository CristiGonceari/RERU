using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.LoggerServices;

namespace CODWER.RERU.Evaluation.Application.Plans.AddPlan
{
    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddPlanCommandHandler> _loggerService;

        public AddPlanCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<AddPlanCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var planToCreate = _mapper.Map<Plan>(request.Data);

            await _appDbContext.Plans.AddAsync(planToCreate);
            await _appDbContext.SaveChangesAsync();
            await LogAction(planToCreate);

            return planToCreate.Id;
        }

        private async Task LogAction(Plan item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Plan was added", item));
        }
    }
}
