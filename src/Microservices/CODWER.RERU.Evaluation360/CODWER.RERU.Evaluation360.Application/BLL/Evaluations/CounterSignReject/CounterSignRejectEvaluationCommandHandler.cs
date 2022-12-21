using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignReject
{
    public class CounterSignRejectEvaluationCommandHandler : IRequestHandler<CounterSignRejectEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CounterSignRejectEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CounterSignRejectEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id);
            evaluation.Status = EvaluationStatusEnum.CounterSignReject;
            evaluation.DateCompletionCounterSigner = System.DateTime.Now;
            evaluation.SignatureCounterSigner = true;
            _mapper.Map(request.Evaluation, evaluation); 
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}