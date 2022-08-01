using CODWER.RERU.Personal.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.References.GetStudyTypes
{
    public class GetStudyTypesQuery : IRequest<List<SelectValue>>
    {
    }
}
