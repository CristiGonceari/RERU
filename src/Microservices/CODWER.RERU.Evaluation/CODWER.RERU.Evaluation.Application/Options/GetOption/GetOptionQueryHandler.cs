using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.GetOption
{
    public class GetOptionQueryHandler : IRequestHandler<GetOptionQuery, OptionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOptionQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<OptionDto> Handle(GetOptionQuery request, CancellationToken cancellationToken)
        {
            var option = await _appDbContext.Options.FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<OptionDto>(option);
        }
    }
}
