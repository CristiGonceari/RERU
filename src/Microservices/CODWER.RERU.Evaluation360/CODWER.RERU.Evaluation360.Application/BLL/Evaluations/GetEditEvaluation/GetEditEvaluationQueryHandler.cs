using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation
{
    public class GetEditEvaluationQueryHandler : IRequestHandler<GetEditEvaluationQuery, GetEvaluationDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetEditEvaluationQueryHandler(AppDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<GetEvaluationDto> Handle(GetEditEvaluationQuery request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations
                .Include(e=> e.EvaluatedUserProfile)
                    .ThenInclude(r=>r.Role)
                .Include(e=> e.EvaluatorUserProfile)
                    .ThenInclude(r=>r.Role)
                .Include(e=> e.CounterSignerUserProfile)
                    .ThenInclude(r=>r.Role)
                .FirstOrDefaultAsync(e=> e.Id == request.Id);
            
            return _mapper.Map<GetEvaluationDto>(evaluation);
        }
    }
}