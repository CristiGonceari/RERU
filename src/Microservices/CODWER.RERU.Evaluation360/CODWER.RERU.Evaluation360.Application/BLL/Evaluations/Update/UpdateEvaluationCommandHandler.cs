using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation360.Application.BLL.Evaluations.GetEditEvaluation;
using CODWER.RERU.Evaluation360.DataTransferObjects.Evaluations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Evaluations.Update
{
    public class UpdateEvaluationCommandHandler : IRequestHandler<UpdateEvaluationCommand, GetEvaluationDto>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ISender _sender;

        public UpdateEvaluationCommandHandler(AppDbContext dbContext, IMapper mapper, ISender sender)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _sender = sender;
        }

        public async Task<GetEvaluationDto> Handle(UpdateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var evaluation = await _dbContext.Evaluations.FirstOrDefaultAsync(e=> e.Id == request.Id );
            _mapper.Map(request.Evaluation, evaluation);
            await _dbContext.SaveChangesAsync();

            await _sender.Send(new GetEditEvaluationQuery(request.Id));

            return _mapper.Map<GetEvaluationDto>(evaluation);
        }
    }
}