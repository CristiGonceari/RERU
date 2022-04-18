using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetTags
{
    public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, List<TagDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTagsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _appDbContext.Tags
                .Where(x => x.Name.Contains(request.Name))
                .ToListAsync();

            return _mapper.Map<List<TagDto>>(tags);
        }
    }
}
