using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositionNotifications;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.AddCandidatePositionNotification
{
    public class AddCandidatePositionNotificationCommandHandler :IRequestHandler<AddCandidatePositionNotificationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddCandidatePositionNotificationCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddCandidatePositionNotificationCommand request, CancellationToken cancellationToken)
        {
            return await AddCandidatePositionNotification(request.Data); 
        }

        private async Task<int> AddCandidatePositionNotification(CandidatePositionNotificationDto data)
        {
            var mappedItem = _mapper.Map<CandidatePositionNotification>(data);

            await _appDbContext.CandidatePositionNotifications.AddAsync(mappedItem);
            await _appDbContext.SaveChangesAsync();

            return mappedItem.UserProfileId;
        }
    }
}
