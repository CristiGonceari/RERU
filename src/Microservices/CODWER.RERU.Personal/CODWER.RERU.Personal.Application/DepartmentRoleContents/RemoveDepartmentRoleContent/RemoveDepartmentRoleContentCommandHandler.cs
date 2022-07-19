using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents.RemoveDepartmentRoleContent
{
    public class RemoveDepartmentRoleContentCommandHandler : IRequestHandler<RemoveDepartmentRoleContentCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveDepartmentRoleContentCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveDepartmentRoleContentCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.DepartmentRoleContents.FirstAsync(dr => dr.Id == request.Id);

            _appDbContext.DepartmentRoleContents.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
