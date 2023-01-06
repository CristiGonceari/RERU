using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.EvaluatedKnow
{
    public class EvaluatedKnowCommandHandler : IRequestHandler<EvaluatedKnowCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public EvaluatedKnowCommandHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EvaluatedKnowCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            evaluation.Status = EvaluationStatusEnum.Finished;
            evaluation.DateEvaluatedKnow = System.DateTime.Now;
            evaluation.SignatureAcknowledgeEvaluated = true;
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}