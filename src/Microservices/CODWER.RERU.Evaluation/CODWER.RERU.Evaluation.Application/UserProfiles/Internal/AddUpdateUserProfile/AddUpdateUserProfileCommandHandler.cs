using System.Linq.Dynamic.Core;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.Internal.AddUpdateUserProfile
{
    public class AddUpdateUserProfileCommandHandler : IRequestHandler<AddUpdateUserProfileCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddUpdateUserProfileCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddUpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var userProfileInEvaluation = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.CoreUserId == request.Data.CoreUserId);

            if (userProfileInEvaluation != null)
            {
                _mapper.Map(request.Data, userProfileInEvaluation);
            }
            else
            {
                var mappedItem = _mapper.Map<UserProfile>(request.Data);
                await _appDbContext.UserProfiles.AddAsync(mappedItem);
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
