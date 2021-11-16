using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetTestTypeValue
{
    public class GetTestTypeValueQuery : IRequest<List<SelectItem>>
    {
    }
}
