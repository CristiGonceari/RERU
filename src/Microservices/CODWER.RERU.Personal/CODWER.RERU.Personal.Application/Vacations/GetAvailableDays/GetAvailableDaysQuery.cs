using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Vacations.GetAvailableDays
{
    [ModuleOperation(permission: PermissionCodes.VACATIONS_GENERAL_ACCESS)]
    public class GetAvailableDaysQuery : IRequest<double>
    {
        public int ContractorId { get; set; }
        public int VacantionTypeId { get; set; }

    }
}
