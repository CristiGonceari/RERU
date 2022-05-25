using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetAddTestProcesses
{
    public class GetAddTestProcessesQuery : IRequest<List<ProcessDataDto>>
    {
    }
}
