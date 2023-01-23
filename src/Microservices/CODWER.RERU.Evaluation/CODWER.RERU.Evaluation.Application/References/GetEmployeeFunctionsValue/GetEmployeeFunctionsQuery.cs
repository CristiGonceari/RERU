using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.References.GetEmployeeFunctionsValue
{
    public class GetEmployeeFunctionsQuery : IRequest<List<SelectItem>>
    {
    }
}
