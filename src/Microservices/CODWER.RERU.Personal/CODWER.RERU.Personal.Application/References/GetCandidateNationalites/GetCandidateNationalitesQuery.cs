using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.References.GetCandidateNationalites
{
    public class GetCandidateNationalitesQuery : IRequest<List<SelectValue>>
    {
    }
}
