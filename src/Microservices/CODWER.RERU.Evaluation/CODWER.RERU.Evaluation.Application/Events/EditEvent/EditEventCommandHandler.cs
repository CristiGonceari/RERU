using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.EditEvent
{
    public class EditEventCommandHandler : IRequestHandler<EditEventCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EditEventCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var editEvent = await _appDbContext.Events.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, editEvent);
            await _appDbContext.SaveChangesAsync();

            return editEvent.Id;
        }
    }
}
