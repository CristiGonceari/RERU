using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.References.GetModernLanguages
{
    public class GetModernLanguageQueryHandler : IRequestHandler<GetModernLanguageQuery, List<SelectValue>>
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public GetModernLanguageQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<SelectValue>> Handle(GetModernLanguageQuery request, CancellationToken cancellationToken)
        {
            var languages = await _appDbContext.ModernLanguages.ToListAsync();

            var mapper = _mapper.Map<List<SelectValue>>(languages);

            return mapper;
        }
    }
}
