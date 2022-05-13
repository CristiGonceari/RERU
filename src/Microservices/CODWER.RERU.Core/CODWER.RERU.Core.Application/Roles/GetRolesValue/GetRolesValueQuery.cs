using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.GetRolesValue
{
    public class GetRolesValueQuery : IRequest<List<SelectItem>>
    {
    }
}
