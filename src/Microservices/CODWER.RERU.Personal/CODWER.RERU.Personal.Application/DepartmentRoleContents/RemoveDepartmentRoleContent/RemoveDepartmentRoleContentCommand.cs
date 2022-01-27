using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.RemoveDepartmentRoleContent
{
    public class RemoveDepartmentRoleContentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
