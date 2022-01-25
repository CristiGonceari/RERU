using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.ContractorDepartments.RemoveContractorDepartment
{
    public class RemoveContractorDepartmentCommandHandler : IRequestHandler<RemoveContractorDepartmentCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveContractorDepartmentCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveContractorDepartmentCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.ContractorDepartments
                .FirstAsync(c => c.Id == request.Id);

            _appDbContext.ContractorDepartments.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
