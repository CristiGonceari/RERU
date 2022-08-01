using AutoMapper;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.UpdateContractorModernLanguageLevel
{
    public class UpdateContractorModernLanguageLevelCommandHandler : IRequestHandler<UpdateContractorModernLanguageLevelCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateContractorModernLanguageLevelCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateContractorModernLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var modernLanguageLevel = _appDbContext.ModernLanguageLevels.FirstOrDefault(mll => mll.Id == request.Data.Id);

            _mapper.Map(request.Data, modernLanguageLevel);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
