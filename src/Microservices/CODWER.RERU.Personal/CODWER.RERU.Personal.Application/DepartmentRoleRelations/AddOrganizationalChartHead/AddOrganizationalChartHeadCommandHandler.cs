using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddOrganizationalChartHead
{
    public class AddOrganizationalChartHeadCommandHandler : IRequestHandler<AddOrganizationalChartHeadCommand, int>
    {
        private readonly AppDbContext _appDbContext;

        public AddOrganizationalChartHeadCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        //head is saved like DepartmentToDepartment or DepartmentToOrganizationRole relation with null ParentId
        public async Task<int> Handle(AddOrganizationalChartHeadCommand request, CancellationToken cancellationToken)
        {
            DepartmentRoleRelation item = new ParentDepartmentChildDepartment();

            switch (request.Type)
            {
                case OrganizationalChartItemType.Department:
                    item = NewDepartmentToDepartmentRelation(request);
                    break;
                case OrganizationalChartItemType.OrganizationRole:
                    item = NewDepartmentToRoleRelation(request);
                    break;
            }

            await _appDbContext.DepartmentRoleRelations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }

        private ParentDepartmentChildDepartment NewDepartmentToDepartmentRelation(AddOrganizationalChartHeadCommand request)
        {
            return new ()
            {
                OrganizationalChartId = request.OrganizationalChartId,
                ChildDepartmentId = request.HeadId
            };
        }

        private ParentDepartmentChildOrganizationRole NewDepartmentToRoleRelation(AddOrganizationalChartHeadCommand request)
        {
            return new()
            {
                OrganizationalChartId = request.OrganizationalChartId,
                ChildOrganizationRoleId = request.HeadId
            };
        }
    }
}
