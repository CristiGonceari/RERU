using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.UpdateContractorDepartment
{
    public class UpdateContractorDepartmentCommandHandler : IRequestHandler<UpdateContractorDepartmentCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UpdateContractorDepartmentCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateContractorDepartmentCommand request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.ContractorDepartments.FirstAsync(c => c.Id == request.Data.Id);

            _mapper.Map(request.Data, item);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
