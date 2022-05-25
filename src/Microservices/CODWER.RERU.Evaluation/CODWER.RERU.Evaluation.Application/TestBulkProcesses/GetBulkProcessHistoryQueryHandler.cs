using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestBulkProcesses
{
    public class GetBulkProcessHistoryQueryHandler : IRequestHandler<GetBulkProcessHistoryQuery, List<HistoryProcessDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetBulkProcessHistoryQueryHandler(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }

        public async Task<List<HistoryProcessDto>> Handle(GetBulkProcessHistoryQuery request, CancellationToken cancellationToken)
        {
            var processes = await _appDbContext.BulkProcesses
                .Where(x => x.ProcessType == Processes.BulkAddTests)
                .AsQueryable()
                .OrderByDescending(x => x.CreateDate)
                .Take(10)
                .Select(p => _mapper.Map<HistoryProcessDto>(p))
                .ToListAsync();

            return processes;
        }
    }
}
