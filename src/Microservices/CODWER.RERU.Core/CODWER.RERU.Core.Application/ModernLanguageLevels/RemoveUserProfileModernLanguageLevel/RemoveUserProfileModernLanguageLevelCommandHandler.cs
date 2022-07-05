using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.RemoveUserProfileModernLanguageLevel
{
    public class RemoveUserProfileModernLanguageLevelCommandHandler : IRequestHandler<RemoveUserProfileModernLanguageLevelCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveUserProfileModernLanguageLevelCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async  Task<Unit> Handle(RemoveUserProfileModernLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var toRemove = _appDbContext.ModernLanguageLevels.FirstOrDefault(mll => mll.Id == request.ModernLanguageLevelId);

            _appDbContext.ModernLanguageLevels.Remove(toRemove);
           await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
