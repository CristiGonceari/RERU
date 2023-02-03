using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentCalculated
{
    public class GetDepartmentRoleContentCalculatedQueryHandler : IRequestHandler<GetDepartmentRoleContentCalculatedQuery, List<DepartmentRoleUserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        public readonly IMapper _mapper;

        public GetDepartmentRoleContentCalculatedQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<DepartmentRoleUserProfileDto>> Handle(GetDepartmentRoleContentCalculatedQuery request, CancellationToken cancellationToken)
        {
            var positions = _appDbContext.UserProfiles
                .Include(p => p.Contractor)
                .Include(p => p.Department)
                .Include(p => p.Role)
                .Include(p => p.EmployeeFunction)
                .AsQueryable();

            if (request.Type == 1)
            {
                positions = positions.Where(p => p.Department.Id == request.Id);
            }
            else if (request.Type == 2)
            {
                positions = positions.Where(p => p.Role.Id == request.Id);

            }

            var items = _mapper.Map<List<DepartmentRoleUserProfileDto>>(positions);

            return items;
        }
    }
}
