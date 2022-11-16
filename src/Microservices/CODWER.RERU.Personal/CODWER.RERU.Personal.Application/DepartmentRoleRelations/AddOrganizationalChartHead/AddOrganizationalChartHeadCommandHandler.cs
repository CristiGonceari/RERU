using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;

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

        //head is saved like DepartmentToDepartment or DepartmentToRole relation with null ParentId
        public async Task<int> Handle(AddOrganizationalChartHeadCommand request, CancellationToken cancellationToken)
        {
            DepartmentRoleRelation item = new ParentDepartmentChildDepartment();

            switch (request.Type)
            {
                case OrganizationalChartItemType.Department:
                    item = NewDepartmentToDepartmentRelation(request);
                    break;
                case OrganizationalChartItemType.Role:
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

            await _loggerService.Log(LogData.AsPersonal($"O nouă relație a fost adăugata în fruntea organigramei {organigram.OrganizationalChart.Name}", departmentRoleRelation));
        }

        private ParentDepartmentChildDepartment NewDepartmentToDepartmentRelation(AddOrganizationalChartHeadCommand request)
        {
            return new ()
            {
                OrganizationalChartId = request.OrganizationalChartId,
                ChildDepartmentId = request.HeadId
            };
        }

        private ParentDepartmentChildRole NewDepartmentToRoleRelation(AddOrganizationalChartHeadCommand request)
        {
            return new()
            {
                OrganizationalChartId = request.OrganizationalChartId,
                ChildRoleId = request.HeadId
            };
        }
    }
}
