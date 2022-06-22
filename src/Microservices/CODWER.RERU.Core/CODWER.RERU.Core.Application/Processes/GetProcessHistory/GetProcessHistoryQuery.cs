using CODWER.RERU.Core.DataTransferObjects.Processes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.Processes.GetProcessHistory
{
    public class GetProcessHistoryQuery : IRequest<List<HistoryProcessesDto>>
    {
    }
}
