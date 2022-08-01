using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.References.GetMaterialStatusType
{
    public class GetMaterialStatusTypeQuery : IRequest<List<SelectValue>>
    {
    }
}
