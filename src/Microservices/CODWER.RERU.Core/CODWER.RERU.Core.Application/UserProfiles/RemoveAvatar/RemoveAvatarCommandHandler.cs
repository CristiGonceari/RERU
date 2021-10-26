using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.RemoveAvatar
{
    public class RemoveAvatarCommandHandler : BaseHandler, IRequestHandler<RemoveAvatarCommand>
    {
        private readonly IDocumentStorageService _documentService;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public RemoveAvatarCommandHandler(ICommonServiceProvider commonServiceProvider, ICurrentApplicationUserProvider currentApplicationUserProvider, IDocumentStorageService documentService) : base(commonServiceProvider)
        {
            _documentService = documentService;
            _currentUserProvider = currentApplicationUserProvider;
        }
        public async Task<Unit> Handle(RemoveAvatarCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserProvider.Get();
            var userProfile = await CoreDbContext
                .UserProfiles
                .Include(up => up.Avatar)
                .FirstOrDefaultAsync(up => up.Id == Convert.ToInt32(currentUser.Id));

            if (userProfile.Avatar != null)
            {
                var document = CoreDbContext.Documents.FirstOrDefault(d => d.DocumentStorageId == userProfile.Avatar.DocumentStorageId);
                await _documentService.Remove(userProfile.Avatar.DocumentStorageId);
                CoreDbContext.Documents.Remove(document);
                await CoreDbContext.SaveChangesAsync();
            }
            return Unit.Value;
        }
    }
}