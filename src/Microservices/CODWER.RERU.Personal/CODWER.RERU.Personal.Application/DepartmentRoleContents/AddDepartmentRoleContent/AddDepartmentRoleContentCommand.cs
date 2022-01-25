using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.AddDepartmentRoleContent
{
    public class AddDepartmentRoleContentCommand : IRequest<int>
    {
        public AddDepartmentRoleContentCommand(AddEditDepartmentRoleContentDto dto)
        {
            Data = dto;
        }

        public AddEditDepartmentRoleContentDto Data { get; set; }
    }
}
