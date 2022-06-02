using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using MediatR;
using System.Collections.Generic;
using CVU.ERP.Module.Application.ImportProcesses;

namespace CODWER.RERU.Evaluation.Application.References.GetAddTestProcesses
{
    public class GetAddTestProcessesQuery : IRequest<List<ProcessDataDto>>
    {
    }
}
