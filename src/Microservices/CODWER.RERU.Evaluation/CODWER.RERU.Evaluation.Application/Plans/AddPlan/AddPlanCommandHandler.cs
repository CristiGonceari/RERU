using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

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
            await _loggerService.Log(LogData.AsEvaluation($@"Planul ""{item.Name}"" a fost adăugat în sistem", item));
        }
    }
}
