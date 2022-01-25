using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentDashboard
{
    public class GetDepartmentDashboardQuery : IRequest<DepartmentDashboardDto>
    {
        public int DepartmentId { get; set; }
    }
}
