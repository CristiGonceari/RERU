using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.RemoveContractorModernLanguageLevel
{
    public class RemoveContractorModernLanguageLevelCommandHandler : IRequestHandler<RemoveContractorModernLanguageLevelCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveContractorModernLanguageLevelCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveContractorModernLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var toRemove = _appDbContext.ModernLanguageLevels.FirstOrDefault(mll => mll.Id == request.ModernLanguageLevelId);

            _appDbContext.ModernLanguageLevels.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
