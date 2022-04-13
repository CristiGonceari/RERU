using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetPositionsSelectValues
{
    public class GetPositionsSelectValuesQuery : IRequest<List<SelectItem>>
    {
    }
}
