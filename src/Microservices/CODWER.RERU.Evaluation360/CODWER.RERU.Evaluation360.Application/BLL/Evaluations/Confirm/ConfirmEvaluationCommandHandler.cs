using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class ConfirmEvaluationCommandHandler : IRequestHandler<ConfirmEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public ConfirmEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sender = sender;
        }

        public async Task<Unit> Handle(ConfirmEvaluationCommand request, CancellationToken cancellationToken)
        {
            await _sender.Send(new UpdateEvaluationCommand(request.Evaluation));
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Evaluation.Id);
            evaluation.Status = EvaluationStatusEnum.Confirmed;
            evaluation.DateCompletionGeneralData = System.DateTime.Now;
            evaluation.SignatureEvaluator = true;
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}