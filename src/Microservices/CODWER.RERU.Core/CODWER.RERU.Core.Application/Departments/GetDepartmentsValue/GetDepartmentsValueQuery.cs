using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Core.Application.Departments.GetDepartmentsValue
{
    public class GetDepartmentsValueQuery : IRequest<List<SelectItem>>
    {
    }
}
