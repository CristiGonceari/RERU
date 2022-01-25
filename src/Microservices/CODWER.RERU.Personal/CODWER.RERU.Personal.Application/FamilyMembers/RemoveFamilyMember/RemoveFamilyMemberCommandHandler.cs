using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.FamilyMembers.RemoveFamilyMember
{
    public class RemoveFamilyMemberCommandHandler : IRequestHandler<RemoveFamilyMemberCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveFamilyMemberCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveFamilyMemberCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.FamilyMembers.FirstAsync(d => d.Id == request.Id);

            _appDbContext.FamilyMembers.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}