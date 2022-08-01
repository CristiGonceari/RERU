using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Autobiographies.GetContractorAutobiography
{
    public class GetContractorAutobiographyQueryHandler : IRequestHandler<GetContractorAutobiographyQuery, AutobiographyDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorAutobiographyQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<AutobiographyDto> Handle(GetContractorAutobiographyQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.Autobiographies
               .FirstAsync(x => x.ContractorId == request.ContractorId);

            return _mapper.Map<AutobiographyDto>(item);
        }
    }
}
