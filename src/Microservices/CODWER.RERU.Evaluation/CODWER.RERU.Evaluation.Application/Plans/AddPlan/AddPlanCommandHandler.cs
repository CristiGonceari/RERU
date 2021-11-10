using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.AddPlan
{
    public class AddPlanCommandHandler : IRequestHandler<AddPlanCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddPlanCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddPlanCommand request, CancellationToken cancellationToken)
        {
            var planToCreate = _mapper.Map<Plan>(request.Data);

            await _appDbContext.Plans.AddAsync(planToCreate);
            await _appDbContext.SaveChangesAsync();

            return planToCreate.Id;
        }

    }

}
