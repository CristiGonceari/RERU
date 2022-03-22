using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.AddOrganizationalChartHead
{
    public class AddOrganizationalChartHeadCommandHandler : IRequestHandler<AddOrganizationalChartHeadCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<AddOrganizationalChartHeadCommand> _loggerService;

        public AddOrganizationalChartHeadCommandHandler(AppDbContext appDbContext, ILoggerService<AddOrganizationalChartHeadCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
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

            await LogAction(item);

            return item.Id;
        }

        private async Task LogAction(DepartmentRoleRelation departmentRoleRelation)
        {
            var organigram = await _appDbContext.DepartmentRoleRelations
                .Include(x => x.OrganizationalChart)
                .FirstAsync(x => x.Id == departmentRoleRelation.Id);

            await _loggerService.Log(LogData.AsPersonal($"New head was added to organigram {organigram.OrganizationalChart.Name}", departmentRoleRelation));
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
