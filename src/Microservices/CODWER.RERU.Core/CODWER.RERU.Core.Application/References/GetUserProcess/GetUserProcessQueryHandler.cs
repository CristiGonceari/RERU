using AutoMapper;
using CVU.ERP.Module.Application.ImportProcesses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.References.GetUserProcess
{
    public class GetUserProcessQueryHandler : IRequestHandler<GetUserProcessQuery, List<ProcessDataDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetUserProcessQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<ProcessDataDto>> Handle(GetUserProcessQuery request, CancellationToken cancellationToken)
        {
            var processes = await _appDbContext.Processes
                .Where(x => x.IsDone == false && x.ProcessesEnumType == ProcessesEnum.BulkAddUsers)
                .AsQueryable()
                .Select(p => _mapper.Map<ProcessDataDto>(p))
                .ToListAsync();

            return processes;
        }
    }
}
