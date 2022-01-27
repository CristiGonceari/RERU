using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Departments.RemoveDepartment
{
    public class RemoveDepartmentCommandHandler : IRequestHandler<RemoveDepartmentCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveDepartmentCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveDepartmentCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Departments.FirstAsync(d => d.Id == request.Id);

            _appDbContext.Departments.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
