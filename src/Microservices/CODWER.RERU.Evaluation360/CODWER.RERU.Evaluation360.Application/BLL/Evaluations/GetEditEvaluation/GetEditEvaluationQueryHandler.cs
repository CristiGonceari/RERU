using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation
{
    public class GetEditEvaluationQueryHandler : IRequestHandler<GetEditEvaluationQuery, EvaluationRowDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetEditEvaluationQueryHandler(AppDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EvaluationRowDto> Handle(GetEditEvaluationQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<EvaluationRowDto>(await _dbContext.Evaluations
                .Include(e=> e.EvaluatedUserProfile)
                .Include(e=> e.EvaluatorUserProfile)
                .Include(e=> e.CounterSignerUserProfile)
                .FirstOrDefaultAsync(e=> e.Id == request.Id));
        }
    }
}