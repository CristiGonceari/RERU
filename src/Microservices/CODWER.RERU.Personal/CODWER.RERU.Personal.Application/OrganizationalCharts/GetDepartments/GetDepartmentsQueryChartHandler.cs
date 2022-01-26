﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetDepartments
{
    public class GetDepartmentsQueryChartHandler : IRequestHandler<GetDepartmentsChartQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDepartmentsQueryChartHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetDepartmentsChartQuery request, CancellationToken cancellationToken)
        {
            var allDepartments = _appDbContext.Departments.AsQueryable();

            var organizationalChartRelations = _appDbContext.DepartmentRoleRelations
                .Where(x => x.OrganizationalChartId == request.OrganizationalChartId)
                .Where(x => x is ParentDepartmentChildDepartment || x is ParentDepartmentChildOrganizationRole || x is ParentOrganizationRoleChildDepartment);

            var organizationalChartDepartments = new List<int>();

            foreach (var relation in organizationalChartRelations)
            {
                if (relation is ParentDepartmentChildDepartment dd)
                {
                    var parentDepartmentId = dd.ParentDepartmentId;

                    if (parentDepartmentId != null)
                        organizationalChartDepartments.Add(parentDepartmentId.Value);

                    var childDepartmentId = dd.ChildDepartmentId;
                        organizationalChartDepartments.Add(childDepartmentId);
                }
                else if (relation is ParentDepartmentChildOrganizationRole dr)
                {
                    var parentDepartmentId = dr.ParentDepartmentId;

                    if (parentDepartmentId != null)
                        organizationalChartDepartments.Add(parentDepartmentId.Value);
                }
                else if (relation is ParentOrganizationRoleChildDepartment od)
                {
                    var childDepartmentId = od.ChildDepartmentId;
                    organizationalChartDepartments.Add(childDepartmentId);
                }
            }

            organizationalChartDepartments = organizationalChartDepartments.Distinct().ToList();

            allDepartments = allDepartments.Where(d => !organizationalChartDepartments.Contains(d.Id));

            return _mapper.Map<List<SelectItem>>(allDepartments);
        }
    }
}