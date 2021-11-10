using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.EditPlan
{
    public class EditPlanCommandHandler : IRequestHandler<EditPlanCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EditPlanCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditPlanCommand request, CancellationToken cancellationToken)
        {
            var planToEdit = await _appDbContext.Plans.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, planToEdit);
            await _appDbContext.SaveChangesAsync();

            return planToEdit.Id;
        }
    }

}
