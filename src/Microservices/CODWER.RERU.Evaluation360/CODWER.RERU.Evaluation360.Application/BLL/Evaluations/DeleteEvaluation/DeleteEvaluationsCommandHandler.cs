using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Evaluation360;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.DeleteEvaluation
{
    public class DeleteEvaluationCommandHandller : IRequestHandler<DeleteEvaluationQuery, Unit>
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public DeleteEvaluationCommandHandller(
            AppDbContext dbContext,  
            ICurrentApplicationUserProvider currentUserProvider, 
            IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteEvaluationQuery request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e => e.Id == request.Id);
            
            _dbContext.Evaluations.Remove(evaluation);
            
            await _dbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}