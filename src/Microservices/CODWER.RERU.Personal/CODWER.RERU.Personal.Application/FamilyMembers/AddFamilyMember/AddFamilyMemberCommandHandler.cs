using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.FamilyMembers.AddFamilyMember
{
    public class AddFamilyMemberCommandHandler : IRequestHandler<AddFamilyMemberCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddFamilyMemberCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var toAdd = _mapper.Map<FamilyMember>(request.Data);

            await _appDbContext.FamilyMembers.AddAsync(toAdd);
            await _appDbContext.SaveChangesAsync();

            return toAdd.Id;
        }
    }
}
