using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation
{
    public class GetEditEvaluationQueryHandler : IRequestHandler<GetEditEvaluationQuery, EditEvaluationDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public GetEditEvaluationQueryHandler(AppDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EditEvaluationDto> Handle(GetEditEvaluationQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<EditEvaluationDto>(await _dbContext.Evaluations.Include(e=> e.EvaluatedUserProfile).FirstOrDefaultAsync(e=> e.Id == request.Id));
        }
    }
}