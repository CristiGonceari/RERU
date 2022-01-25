using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contacts.RemoveContact
{
    public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveContactCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _appDbContext.Contacts.FirstAsync(rt => rt.Id == request.Id);

            _appDbContext.Contacts.Remove(contact);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
