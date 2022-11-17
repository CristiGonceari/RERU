using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddDepartmentRoleRelation
{
    public class AddDepartmentRoleRelationCommandHandler : IRequestHandler<AddDepartmentRoleRelationCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<AddDepartmentRoleRelationCommand> _loggerService;

        public AddDepartmentRoleRelationCommandHandler(AppDbContext appDbContext, ILoggerService<AddDepartmentRoleRelationCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddDepartmentRoleRelationCommand request, CancellationToken cancellationToken)
        {
            DepartmentRoleRelation item = new ParentDepartmentChildDepartment();

            switch (request.Data.RelationType)
            {
                case DepartmentRoleRelationTypeEnum.DepartmentDepartment:
                    item = NewDepartmentToDepartmentRelation(request);
                    break;
                case DepartmentRoleRelationTypeEnum.DepartmentRole:
                    item = NewDepartmentToRoleRelation(request);
                    break;
                case DepartmentRoleRelationTypeEnum.RoleDepartment:
                    item = NewRoleToDepartmentRelation(request);
                    break;
                case DepartmentRoleRelationTypeEnum.RoleRole:
                    item = NewRoleToRoleRelation(request);
                    break;
            }

            await _appDbContext.DepartmentRoleRelations.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return item.Id;
        }

        private async Task LogAction(DepartmentRoleRelation departmentRoleRelation)
        {
            var organigram = await _appDbContext.DepartmentRoleRelations
                .Include(x => x.OrganizationalChart)
                .FirstAsync(x => x.Id == departmentRoleRelation.Id);

            await _loggerService.Log(LogData.AsPersonal($"O nouă relație a fost adăugata la organigrama {organigram.OrganizationalChart.Name}", departmentRoleRelation));
        }

        private ParentDepartmentChildDepartment NewDepartmentToDepartmentRelation(AddDepartmentRoleRelationCommand request)
        {
            return new ()
            {
                OrganizationalChartId = request.Data.OrganizationalChartId,
                ParentDepartmentId = request.Data.ParentId,
                ChildDepartmentId = request.Data.ChildId
            };
        }

        private ParentDepartmentChildRole NewDepartmentToRoleRelation(AddDepartmentRoleRelationCommand request)
        {
            return new()
            {
                OrganizationalChartId = request.Data.OrganizationalChartId,
                ParentDepartmentId = request.Data.ParentId,
                ChildRoleId = request.Data.ChildId
            };
        }

        private ParentRoleChildDepartment NewRoleToDepartmentRelation(AddDepartmentRoleRelationCommand request)
        {
            return new()
            {
                OrganizationalChartId = request.Data.OrganizationalChartId,
                ParentRoleId = request.Data.ParentId,
                ChildDepartmentId = request.Data.ChildId
            };
        }

        private ParentRoleChildRole NewRoleToRoleRelation(AddDepartmentRoleRelationCommand request)
        {
            return new()
            {
                OrganizationalChartId = request.Data.OrganizationalChartId,
                ParentRoleId = request.Data.ParentId,
                ChildRoleId = request.Data.ChildId
            };
        }
    }
}
