using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation
{
    public class GetEditEvaluationQueryHandler : IRequestHandler<GetEditEvaluationQuery, EvaluationRowDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        
        public GetEditEvaluationQueryHandler(AppDbContext dbContext, ICurrentApplicationUserProvider currentUserProvider, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<EvaluationRowDto> Handle(GetEditEvaluationQuery request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            var currentUser = await _currentUserProvider.Get();
            var currentUserId = int.Parse(currentUser.Id);

            if (currentUserId == evaluation.EvaluatorUserProfileId && evaluation.Status == EvaluationStatusEnum.Draft) 
            {
                evaluation.canEvaluate = true;
                evaluation.canDelete = true;
            }

            if (currentUserId == evaluation.EvaluatedUserProfileId && evaluation.Status == EvaluationStatusEnum.Confirmed)
            { 
                evaluation.canAccept = true;
            }

            if (currentUserId == evaluation.CounterSignerUserProfileId && evaluation.Status == EvaluationStatusEnum.Accepted)
            { 
                evaluation.canCounterSign = true;
            }

            if (currentUserId == evaluation.EvaluatedUserProfileId && evaluation.Status == EvaluationStatusEnum.CounterSignAccept)
            { 
                evaluation.canFinished = true;
            }

            return _mapper.Map<EvaluationRowDto>(await _dbContext.Evaluations
                .Include(e=> e.EvaluatedUserProfile)
                .Include(e=> e.EvaluatorUserProfile)
                .Include(e=> e.CounterSignerUserProfile)
                .FirstOrDefaultAsync(e=> e.Id == request.Id));
        }
    }
}