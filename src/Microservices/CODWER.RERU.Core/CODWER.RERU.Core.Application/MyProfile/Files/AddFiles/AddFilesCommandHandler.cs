﻿using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.MyProfile.Files.AddFiles
{
    public class AddFilesCommandHandler : BaseHandler, IRequestHandler<AddFilesCommand, string>
    {
        private readonly IStorageFileService _storageFileService;
        private readonly ICurrentApplicationUserProvider _currentApplication;

        public AddFilesCommandHandler(ICommonServiceProvider commonServiceProvider,
            IStorageFileService storageFileService, 
            ICurrentApplicationUserProvider currentApplication) : base(commonServiceProvider)
        {
            _storageFileService = storageFileService;
            _currentApplication = currentApplication;
        }

        public async Task<string> Handle(AddFilesCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplication.Get();

            var fileId = await _storageFileService.AddFile(request.File);

            var userFile = new UserFile(int.Parse(currentUser.Id), fileId);

            await CoreDbContext.UserFiles.AddAsync(userFile);
            await CoreDbContext.SaveChangesAsync();

            return fileId;
        }

    }
}
