using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;

namespace CODWER.RERU.Evaluation.Application.Options.GetOptions
{
    public class GetOptionsQueryHandler : IRequestHandler<GetOptionsQuery, List<OptionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetOptionsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<OptionDto>> Handle(GetOptionsQuery request, CancellationToken cancellationToken)
        {
            var options = _appDbContext.Options.AsQueryable();

            if (request.QuestionUnitId.HasValue)
            {
                options = options.Where(x => x.QuestionUnitId == request.QuestionUnitId.Value);
            }

            var answer = await options.ToListAsync();

            return _mapper.Map<List<OptionDto>>(answer);
        }
    }
}
