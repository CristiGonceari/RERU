using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using MediatR;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.GetBulkImportProcess
{
    public class GetBulkImportProcessQueryHandler : IRequestHandler<GetBulkImportProcessQuery, ProcessDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetBulkImportProcessQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ProcessDataDto> Handle(GetBulkImportProcessQuery request, CancellationToken cancellationToken)
        {
            var process = _appDbContext.BulkProcesses.FirstOrDefault(x => x.Id == request.ProcessId && x.ProcessType == Processes.BulkAddTests);

            return _mapper.Map<ProcessDataDto>(process);
        }
    }
}
