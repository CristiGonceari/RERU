using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles.AddKinshipRelationWithUserProfile
{
    public class AddKinshipRelationWithUserProfileCommandHandler : IRequestHandler<AddKinshipRelationWithUserProfileCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddKinshipRelationWithUserProfileCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddKinshipRelationWithUserProfileCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<KinshipRelationWithUserProfile>(request.Data);

            await _appDbContext.KinshipRelationWithUserProfiles.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
