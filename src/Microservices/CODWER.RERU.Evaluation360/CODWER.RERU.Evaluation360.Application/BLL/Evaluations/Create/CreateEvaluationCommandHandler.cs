using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.ServiceProvider;
using MediatR;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Create
{
    public class CreateEvaluationCommandHandler : IRequestHandler<CreateEvaluationCommand, int>
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public CreateEvaluationCommandHandler(AppDbContext dbContext,  ICurrentApplicationUserProvider currentUserProvider, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEvaluationCommand request, CancellationToken cancellationToken)
        {
            // if(request.EvaluatedUserProfileId == request.CounterSignerUserProfileId)
            //     throw new Exception("Nu pot fi egale");


            var currentUser = await _currentUserProvider.Get();
            var currentuserId = int.Parse(currentUser.Id);

            // if(request.EvaluatedUserProfileId == currentuserId)
            //     throw new Exception("Nu pot fi egale");


            var newEvaluation = _mapper.Map<Evaluation>(request);
            newEvaluation.EvaluatorUserProfileId = currentuserId;
        
           _dbContext.Evaluations.Add(newEvaluation);
           await _dbContext.SaveChangesAsync();
           return newEvaluation.Id;
        }
    }
}