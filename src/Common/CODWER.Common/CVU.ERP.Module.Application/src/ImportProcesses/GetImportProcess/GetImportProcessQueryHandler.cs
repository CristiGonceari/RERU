using AutoMapper;
using MediatR;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CVU.ERP.Module.Application.ImportProcesses.GetImportProcess
{
    public class GetImportProcessQueryHandler : IRequestHandler<GetImportProcessQuery, ProcessDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetImportProcessQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ProcessDataDto> Handle(GetImportProcessQuery request, CancellationToken cancellationToken)
        {
            var process = _appDbContext.Processes.FirstOrDefault(x => x.Id == request.ProcessId);

            return _mapper.Map<ProcessDataDto>(process);
        }
    }
}
