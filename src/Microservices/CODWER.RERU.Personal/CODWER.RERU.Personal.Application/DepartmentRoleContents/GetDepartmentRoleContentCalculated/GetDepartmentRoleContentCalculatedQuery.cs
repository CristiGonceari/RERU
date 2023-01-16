using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQuery : IRequest<DepartmentRoleContentDto>
    {
        public int Id { get; set; }
        public int Type { get; set; }
    }
}