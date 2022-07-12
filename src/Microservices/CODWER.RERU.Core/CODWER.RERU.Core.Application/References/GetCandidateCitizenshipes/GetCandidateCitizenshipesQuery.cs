using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.References.GetCandidateCitizenshipes
{
    public class GetCandidateCitizenshipesQuery : IRequest<List<SelectValue>>
    {
    }
}
