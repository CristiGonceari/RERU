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
using CVU.ERP.Module.Application.ImportProcesses;

namespace CODWER.RERU.Evaluation.Application.References.GetAddTestProcesses
{
    public class GetAddTestProcessesQueryHandler : IRequestHandler<GetAddTestProcessesQuery, List<ProcessDataDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAddTestProcessesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<ProcessDataDto>> Handle(GetAddTestProcessesQuery request, CancellationToken cancellationToken)
        {
            var processes = await _appDbContext.Processes
                .Where(x => x.IsDone == false && x.ProcessesEnumType == ProcessesEnum.BulkAddTests)
                .AsQueryable()
                .Select(p => _mapper.Map<ProcessDataDto>(p))
                .ToListAsync();

            return processes;
        }
    }
}
