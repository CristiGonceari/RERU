using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.AddDepartmentRoleContent
{
    public class AddDepartmentRoleContentCommandHandler : IRequestHandler<AddDepartmentRoleContentCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddDepartmentRoleContentCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddDepartmentRoleContentCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<DepartmentRoleContent>(request.Data);

            await _appDbContext.DepartmentRoleContents.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            return item.Id;
        }
    }
}
