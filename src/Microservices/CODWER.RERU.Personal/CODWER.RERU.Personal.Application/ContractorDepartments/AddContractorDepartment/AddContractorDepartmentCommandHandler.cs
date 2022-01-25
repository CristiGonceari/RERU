using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.AddContractorDepartment
{
    public class AddContractorDepartmentCommandHandler : IRequestHandler<AddContractorDepartmentCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddContractorDepartmentCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddContractorDepartmentCommand request, CancellationToken cancellationToken)
        {
            var toAdd = _mapper.Map<ContractorDepartment>(request.Data);

            await _appDbContext.ContractorDepartments.AddAsync(toAdd);
            await _appDbContext.SaveChangesAsync();

            return toAdd.Id;
        }
    }
}
