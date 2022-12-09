using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.ServiceProvider;
using MediatR;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create
{
    public class CreateEvaluationsCommandHandler : IRequestHandler<CreateEvaluationsCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public CreateEvaluationsCommandHandler(AppDbContext dbContext,  ICurrentApplicationUserProvider currentUserProvider, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateEvaluationsCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserProvider.Get();
            var currentuserId = int.Parse(currentUser.Id);
            
            foreach(var evaluatedUserId in request.EvaluatedUserProfileIds)
            {
                var newEvaluation = _mapper.Map<Evaluation>(request);
                
                newEvaluation.EvaluatedUserProfileId = evaluatedUserId;
                newEvaluation.EvaluatorUserProfileId = currentuserId;

                _dbContext.Evaluations.Add(newEvaluation);
            }
          
           await _dbContext.SaveChangesAsync();
           return Unit.Value;
        }
        
    }
}