using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;
namespace CODWER.RERU.Evaluation360.Application.References.GetEmployeeFunctions
{
    public class GetEmployeeFunctionsQuery : IRequest<List<SelectItem>>
    {
    }
}