using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.RemoveVacation
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_VACANTE)]

    public class RemoveVacationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
