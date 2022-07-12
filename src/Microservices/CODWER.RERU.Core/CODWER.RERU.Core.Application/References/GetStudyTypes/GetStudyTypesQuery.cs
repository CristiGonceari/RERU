using CODWER.RERU.Core.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.References.GetStudyTypes
{
    public class GetStudyTypesQuery : IRequest<List<SelectValue>>
    {
    }
}
