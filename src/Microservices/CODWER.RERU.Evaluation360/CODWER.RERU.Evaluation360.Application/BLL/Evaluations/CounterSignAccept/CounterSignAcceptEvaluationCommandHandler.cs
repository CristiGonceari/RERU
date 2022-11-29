using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.CounterSignAccept
{
    public class CounterSignAcceptEvaluationCommandHandler : IRequestHandler<CounterSignAcceptEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CounterSignAcceptEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CounterSignAcceptEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Evaluation.Id);
            evaluation.Status = EvaluationStatusEnum.CounterSignAccept;
            _mapper.Map(request.Evaluation, evaluation); 
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}