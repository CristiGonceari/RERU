using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Processes;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Processes.GetProcessHistory
{
    public class GetProcessHistoryQueryHandler : IRequestHandler<GetProcessHistoryQuery, List<HistoryProcessesDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetProcessHistoryQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<HistoryProcessesDto>> Handle(GetProcessHistoryQuery request, CancellationToken cancellationToken)
        {
            var processes = await _appDbContext.Processes
                .Where(x => x.ProcessesEnumType == ProcessesEnum.BulkAddUsers)
                .AsQueryable()
                .OrderByDescending(x => x.CreateDate)
                .Take(10)
                .Select(p => _mapper.Map<HistoryProcessesDto>(p))
                .ToListAsync();

            return processes;
        }
    }
}
