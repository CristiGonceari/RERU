using System.Collections.Generic;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CVU.ERP.ServiceProvider;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Users.ChangePersonalData
{
    public class ChangePersonalDataCommandHandler : BaseHandler, IRequestHandler<ChangePersonalDataCommand, Unit>
    {
        private readonly ICurrentApplicationUserProvider _userProvider;
        private readonly IEnumerable<IIdentityService> _identityServices;

        public ChangePersonalDataCommandHandler(ICommonServiceProvider commonServiceProvider, ICurrentApplicationUserProvider userProvider, IEnumerable<IIdentityService> identityServices) : base(commonServiceProvider)
        {
            _userProvider = userProvider;
            _identityServices = identityServices;
        }

        public async Task<Unit> Handle(ChangePersonalDataCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProvider.Get();

            var userProfile = await AppDbContext.UserProfiles.FirstOrDefaultAsync(up => up.Id == int.Parse(currentUser.Id));

            if (request.User.Email != userProfile.Email)
            {
                foreach (var identityService in _identityServices)
                {
                    var userName = $"{request.User.LastName} {request.User.FirstName} {request.User.FatherName}";

                    var identifier = await identityService.Update(userName, request.User.Email, userProfile.Email, true);

                    if (!string.IsNullOrEmpty(identifier))
                    {
                        userProfile.Identities.Add(new UserProfileIdentity
                        {
                            Identificator = identifier,
                            Type = identityService.Type
                        });
                    }
                }
            }

            Mapper.Map(request.User, userProfile);

            await AppDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}