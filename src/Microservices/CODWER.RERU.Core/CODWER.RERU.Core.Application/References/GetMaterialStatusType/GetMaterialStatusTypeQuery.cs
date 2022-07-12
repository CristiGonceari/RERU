using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.References.GetMaterialStatusType
{
    public class GetMaterialStatusTypeQuery : IRequest<List<SelectValue>>
    {
    }
}
