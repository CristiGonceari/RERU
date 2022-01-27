using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentTemplate
{
    public class GetDepartmentRoleContentTemplateQuery : IRequest<DepartmentRoleContentDto>
    {
        public int DepartmentId { get; set; }
    }
}
