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

            var positions = _appDbContext.UserProfiles
                .Include(p => p.Contractor)
                .Include(p => p.Department)
                .Include(p => p.Role)
                .AsQueryable();
            //.Where(p =>
            //    ((p.FromDate == null && p.ToDate == null)
            //     || (p.ToDate == null && p.FromDate != null && p.FromDate < now)
            //     || (p.FromDate == null && p.ToDate != null && p.ToDate > now)
            //     || (p.FromDate != null && p.ToDate != null && p.FromDate < now && p.ToDate > now)));

            if (request.Type == 1)
            {
                positions = positions.Where(p => p.Department.Id == request.Id);
            }
            else if (request.Type == 2)
            {
                positions = positions.Where(p => p.Role.Id == request.Id);

            }

            var departmentContent = positions
                .ToList()
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

                            OrganizationRoleCount = rfd.ToList().Select(c => c.Id).Distinct().Count(),
                            ContractorIds = rfd.ToList().Select(c => new SelectItem
                            {
                                Label = c.FullName,
                                Value = c.Id.ToString()
                            }).Distinct().ToList()
                        }).ToList()
                }).ToList();

            return departmentContent.Count() != 0
                ? departmentContent.First()
                : new DepartmentRoleContentDto();
        }
    }
}
