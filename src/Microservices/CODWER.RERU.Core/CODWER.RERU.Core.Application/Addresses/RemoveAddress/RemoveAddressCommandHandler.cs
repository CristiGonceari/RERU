using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Addresses.RemoveAddress
{
    public class RemoveAddressCommandHandler : IRequestHandler<RemoveAddressCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveAddressCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Addresses.FirstAsync(a => a.Id == request.Id);

            _appDbContext.Addresses.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
