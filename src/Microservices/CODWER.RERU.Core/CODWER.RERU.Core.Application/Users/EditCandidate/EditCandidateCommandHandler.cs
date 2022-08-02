using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.EditCandidate
{
    public class EditCandidateCommandHandler : IRequestHandler<EditCandidateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EditCandidateCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> Handle(EditCandidateCommand request, CancellationToken cancellationToken)
        {
            var userProfile = _appDbContext.Contractors
                                            .Include(up => up.UserProfile)
                                            .FirstOrDefault(uf => uf.Id == request.Data.Id);
                                             

            _mapper.Map(request.Data, userProfile);
            await _appDbContext.SaveChangesAsync();

            return userProfile.Id;
           
        }
    }
}
