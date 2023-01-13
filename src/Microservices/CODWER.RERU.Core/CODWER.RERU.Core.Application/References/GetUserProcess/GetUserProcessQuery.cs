using CVU.ERP.Module.Application.ImportProcessServices.ImportProcessModels;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.References.GetUserProcess
{
    public class GetUserProcessQuery : IRequest<List<ProcessDataDto>>
    {
    }
}
