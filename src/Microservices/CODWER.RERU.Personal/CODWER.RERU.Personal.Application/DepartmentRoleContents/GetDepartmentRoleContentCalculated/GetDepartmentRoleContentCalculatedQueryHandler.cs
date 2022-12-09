using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQueryHandler : IRequestHandler<GetDepartmentRoleContentCalculatedQuery, DepartmentRoleContentDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public GetDepartmentRoleContentCalculatedQueryHandler(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<DepartmentRoleContentDto> Handle(GetDepartmentRoleContentCalculatedQuery request, CancellationToken cancellationToken)
        {
            var now = _dateTime.Now;

            var positions = _appDbContext.Positions
                .Include(p => p.Contractor)
                .Include(p => p.Department)
                .Include(p => p.Role)
                .Where(p => p.DepartmentId == request.DepartmentId)
                .ToList()
                .Where(p =>
                    ((p.FromDate == null && p.ToDate == null)
                     || (p.ToDate == null && p.FromDate != null && p.FromDate < now)
                     || (p.FromDate == null && p.ToDate != null && p.ToDate > now)
                     || (p.FromDate != null && p.ToDate != null && p.FromDate < now && p.ToDate > now)));

            var departmentContent = positions
                .GroupBy(p => p.Department) // group by department
                .Select(drc => new DepartmentRoleContentDto
                {
                    DepartmentId = drc.Key.Id,
                    DepartmentName = drc.Key.Name,

                    Roles = drc
                        .GroupBy(p=>p.Role) // group by organization role
                        .Select(rfd=> new RoleFromDepartment
                        {
                            OrganizationRoleId = rfd.Key.Id,
                            OrganizationRoleName = rfd.Key.Name,

                            OrganizationRoleCount = rfd.ToList().Select(c => c.ContractorId).Distinct().Count(),
                            ContractorIds = rfd.ToList().Select(c => new SelectItem
                            {
                                Label = c.Contractor.GetFullName(),
                                Value = c.ContractorId.ToString()
                            }).Distinct().ToList()
                        }).ToList()
                }).ToList();

            return departmentContent.Count() != 0
                ? departmentContent.First()
                : new DepartmentRoleContentDto();
        }
    }
}
