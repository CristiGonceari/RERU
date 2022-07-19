using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.GetDepartmentRoleContentTemplate
{
    public class GetDepartmentRoleContentTemplateQueryHandler : IRequestHandler<GetDepartmentRoleContentTemplateQuery, DepartmentRoleContentDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetDepartmentRoleContentTemplateQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<DepartmentRoleContentDto> Handle(GetDepartmentRoleContentTemplateQuery request, CancellationToken cancellationToken)
        {
            var department = await _appDbContext.Departments
                .Include(d => d.DepartmentRoleContents)
                .ThenInclude(x => x.Role)
                .FirstAsync(d => d.Id == request.DepartmentId);

            var departmentRoleContent = _mapper.Map<DepartmentRoleContentDto>(department);

            return departmentRoleContent;
        }
    }
}
