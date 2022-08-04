using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.References.GetCandidateCitizenshipes
{
    public class GetCandidateCitizenshipesQuery : IRequest<List<SelectValue>>
    {
    }
}
