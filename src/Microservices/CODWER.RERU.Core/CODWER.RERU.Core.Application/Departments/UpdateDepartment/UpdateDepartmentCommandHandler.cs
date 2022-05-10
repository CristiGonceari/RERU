using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.UpdateDepartment
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var newDepart = new DepartmentDto()
            {
                Id = request.Id,
                Name = request.Name,
                ColaboratorId = request.ColaboratorId
            };

            var department = await _appDbContext.Departments.FirstAsync(x => x.Id == newDepart.Id);

            _mapper.Map(newDepart, department);
            await _appDbContext.SaveChangesAsync();

            return department.Id;
        }
    }
}
