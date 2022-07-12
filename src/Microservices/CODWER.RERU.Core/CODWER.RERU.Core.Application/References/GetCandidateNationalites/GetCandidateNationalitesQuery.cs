using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.References.GetCandidateNationalites
{
    public class GetCandidateNationalitesQuery : IRequest<List<SelectValue>>
    {
    }
}
