using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.Departments.AddDepartment
{
    public class AddDepartmentCommandHandler : IRequestHandler<AddDepartmentCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddDepartmentCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {
            var toAdd = _mapper.Map<Department>(request.Data);

            await _appDbContext.Departments.AddAsync(toAdd);
            await _appDbContext.SaveChangesAsync();

            return toAdd.Id;
        }
    }
}
