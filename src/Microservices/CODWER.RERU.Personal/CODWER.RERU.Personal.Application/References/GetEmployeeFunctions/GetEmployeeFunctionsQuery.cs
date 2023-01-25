using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.References.GetEmployeeFunctions
{
    public class GetEmployeeFunctionsQuery : IRequest<List<SelectItem>>
    {
    }
}
