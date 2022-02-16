using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Core.Application.UserProfiles.UploadAvatar
{
    public class UploadAvatarCommandHandler : BaseHandler, IRequestHandler<UploadAvatarCommand, Unit>
    {
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IDocumentStorageService _documentService;

        public UploadAvatarCommandHandler(ICommonServiceProvider commonServiceProvider, IDocumentStorageService documentService, ICurrentApplicationUserProvider currentApplicationUserProvider) : base(commonServiceProvider)
        {
            _documentService = documentService;
            _currentUserProvider = currentApplicationUserProvider;
        }

        public async Task<Unit> Handle(UploadAvatarCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserProvider.Get();
            var userProfile = await CoreDbContext.UserProfiles.Include(up => up.Avatar).FirstOrDefaultAsync(up => up.Id == int.Parse(currentUser.Id));
            var avatar = new Document();
            Guid oldDocumentId = new Guid();
            var hadAvatar = false;

            if (request.File != null)
            {
                if (userProfile.Avatar != null)
                {
                    hadAvatar = true;
                    oldDocumentId = userProfile.Avatar.DocumentStorageId;
                }

                avatar.Name = request.File.Name;
                avatar.Type = request.File.ContentType;

                var memoryStream = new MemoryStream();
                await request.File.CopyToAsync(memoryStream);
                avatar.DocumentStorageId = Guid.Parse(await _documentService.UploadDocument(memoryStream.ToArray()));

                if (hadAvatar == true)
                {
                    var document = CoreDbContext.Documents.FirstOrDefault(d => d.DocumentStorageId == oldDocumentId);
                    await _documentService.Remove(oldDocumentId);
                    CoreDbContext.Documents.Remove(document);
                }

                userProfile.Avatar = avatar;
                CoreDbContext.Documents.Add(avatar);
                await CoreDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("No File found");
            }

            return Unit.Value;
        }
    }
}