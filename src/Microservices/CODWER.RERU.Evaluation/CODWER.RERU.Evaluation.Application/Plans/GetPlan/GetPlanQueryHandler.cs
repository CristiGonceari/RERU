using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Plans.GetPlan
{
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery, PlanDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPlanQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PlanDto> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var plan = await _appDbContext.Plans
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<PlanDto>(plan);
        }
    }

}
