using AutoMapper;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.AddModernLanguageLevel
{
    public class AddModernLanguageLevelCommandHandler : IRequestHandler<AddModernLanguageLevelCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddModernLanguageLevelCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddModernLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            var language = _mapper.Map<ModernLanguageLevel>(request.Data);

            await _appDbContext.ModernLanguageLevels.AddAsync(language);
            await _appDbContext.SaveChangesAsync();

            return language.Id;
        }
    }
}
