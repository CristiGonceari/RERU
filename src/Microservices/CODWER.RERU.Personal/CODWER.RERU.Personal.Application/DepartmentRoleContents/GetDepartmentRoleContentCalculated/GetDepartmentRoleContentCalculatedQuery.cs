using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQuery : IRequest<DepartmentRoleContentDto>
    {
        public int DepartmentId { get; set; }
    }
}