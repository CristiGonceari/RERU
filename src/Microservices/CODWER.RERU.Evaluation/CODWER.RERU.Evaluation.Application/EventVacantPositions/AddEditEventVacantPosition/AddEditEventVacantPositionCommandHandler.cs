using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventVacantPositions.AddEditEventVacantPosition
{
    public class AddEditEventVacantPositionCommandHandler : IRequestHandler<AddEditEventVacantPositionCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddEditEventVacantPositionCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddEditEventVacantPositionCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.EventVacantPositions
                .FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            if (item != null)
            {
                _mapper.Map(request.Data, item);
                await _appDbContext.SaveChangesAsync();

                return item.Id;
            }

            var newItem = _mapper.Map<EventVacantPosition>(request.Data);

            await _appDbContext.EventVacantPositions.AddAsync(newItem);
            await _appDbContext.SaveChangesAsync();

            return newItem.Id;
        }
    }
}
