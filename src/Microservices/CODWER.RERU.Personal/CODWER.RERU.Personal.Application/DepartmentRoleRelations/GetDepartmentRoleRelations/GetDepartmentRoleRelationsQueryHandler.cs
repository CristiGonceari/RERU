using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
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

                var roles = _appDbContext.ParentDepartmentChildRoles
                    .Include(p=>p.ChildRole)
                    .Where(x => x.ParentDepartmentId == request.ParentDepartmentId);

                response = new DepartmentRoleRelationDto
                {
                    Departments = _mapper.Map<List<DepartmentRelationDto>>(departments),
                    Roles = _mapper.Map<List<RoleRelationDto>>(roles)
                };
            }
            else if (request.ParentRoleId != null)
            {
                var departments = _appDbContext.ParentRoleChildDepartments
                    .Include(p => p.ChildDepartment)
                    .Where(d => d.ParentRoleId == request.ParentRoleId);

                var roles = _appDbContext.ParentRoleChildRoles
                    .Include(p => p.ChildRole)
                    .Where(x => x.ParentRoleId == request.ParentRoleId);

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
