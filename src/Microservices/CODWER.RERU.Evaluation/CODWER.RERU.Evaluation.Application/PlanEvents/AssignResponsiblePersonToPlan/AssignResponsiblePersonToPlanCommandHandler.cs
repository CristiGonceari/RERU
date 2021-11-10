using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.AssignResponsiblePersonToPlan
{
    public class AssignResponsiblePersonToPlanCommandHandler : IRequestHandler<AssignResponsiblePersonToPlanCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignResponsiblePersonToPlanCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AssignResponsiblePersonToPlanCommand request, CancellationToken cancellationToken)
        {
            var planResponsiblePerson = _mapper.Map<PlanResponsiblePerson>(request.Data);

            await _appDbContext.PlanResponsiblePersons.AddAsync(planResponsiblePerson);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }

}
