using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.GetDepartmentRoleRelations
{
    public class GetDepartmentRoleRelationsQueryHandler : IRequestHandler<GetDepartmentRoleRelationsQuery, DepartmentRoleRelationDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDepartmentRoleRelationsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<DepartmentRoleRelationDto> Handle(GetDepartmentRoleRelationsQuery request, CancellationToken cancellationToken)
        {
            var response = new DepartmentRoleRelationDto();

            if (request.ParentDepartmentId != null)
            {
                var departments = _appDbContext.ParentDepartmentChildDepartments
                    .Include(p=>p.ChildDepartment)
                    .Where(d => d.ParentDepartmentId == request.ParentDepartmentId);

                var roles = _appDbContext.ParentDepartmentChildOrganizationRoles
                    .Include(p=>p.ChildOrganizationRole)
                    .Where(x => x.ParentDepartmentId == request.ParentDepartmentId);

                response = new DepartmentRoleRelationDto
                {
                    Departments = _mapper.Map<List<DepartmentRelationDto>>(departments),
                    Roles = _mapper.Map<List<RoleRelationDto>>(roles)
                };
            }
            else if (request.ParentOrganizationRoleId != null)
            {
                var departments = _appDbContext.ParentOrganizationRoleChildDepartments
                    .Include(p => p.ChildDepartment)
                    .Where(d => d.ParentOrganizationRoleId == request.ParentOrganizationRoleId);

                var roles = _appDbContext.ParentOrganizationRoleChildOrganizationRoles
                    .Include(p => p.ChildOrganizationRole)
                    .Where(x => x.ParentOrganizationRoleId == request.ParentOrganizationRoleId);

                response = new DepartmentRoleRelationDto
                {
                    Departments = _mapper.Map<List<DepartmentRelationDto>>(departments),
                    Roles = _mapper.Map<List<RoleRelationDto>>(roles)
                };
            }

            return response;
        }
    }
}
