using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails {
    public class EditUserPersonalDetailsCommandHandler : BaseHandler, IRequestHandler<EditUserPersonalDetailsCommand, Unit> {
        public EditUserPersonalDetailsCommandHandler (ICommonServiceProvider commonServiceProvider) : base (commonServiceProvider) { }

        public async Task<Unit> Handle (EditUserPersonalDetailsCommand request, CancellationToken cancellationToken) {
            var userProfile = await CoreDbContext.UserProfiles.FirstOrDefaultAsync (up => up.Id == request.Data.Id);
            Mapper.Map (request.Data, userProfile);
            await CoreDbContext.SaveChangesAsync ();
            return Unit.Value;
        }
    }
}