using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class UpdateEvaluationCommandHandler : IRequestHandler<UpdateEvaluationCommand, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id );
            _mapper.Map(request.Evaluation, evaluation);
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}