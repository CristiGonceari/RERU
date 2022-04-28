using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.RemoveDepartment
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
            var department = await _appDbContext.Departments.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Departments.Remove(department);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
