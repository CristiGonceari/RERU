using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.SaveUserPasswordByEmail
{
    public class SaveUserPasswordByEmailCommandHandler : IRequestHandler<SaveUserPasswordByEmailCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordGenerator _passwordGenerator;
        private readonly IEnumerable<IIdentityService> _identityServices;
        private const string DEFAULT_IDENTITY_SERVICE = "local";

        public SaveUserPasswordByEmailCommandHandler(AppDbContext appDbContext, IPasswordGenerator passwordGenerator, IEnumerable<IIdentityService> identityServices)
        {
            _appDbContext = appDbContext;
            _passwordGenerator = passwordGenerator;
            _identityServices = identityServices;
        }

        public async Task<Unit> Handle(SaveUserPasswordByEmailCommand request, CancellationToken cancellationToken)
        {
            var userProfile = await _appDbContext.UserProfiles
                .Include(up => up.Identities)
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (userProfile != null && string.IsNullOrEmpty(userProfile.Password))
            {
                var password = _passwordGenerator.Generate();

                var identityService = _identityServices.FirstOrDefault(@is => @is.Type == DEFAULT_IDENTITY_SERVICE);
                if (identityService != null)
                {
                    var identity = userProfile.Identities.FirstOrDefault(upi => upi.Type == DEFAULT_IDENTITY_SERVICE);
                    if (identity != null)
                    {
                        await identityService.ResetPassword(identity.Identificator, password, false);
                    }
                }

                userProfile.Password = password;
                await _appDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
