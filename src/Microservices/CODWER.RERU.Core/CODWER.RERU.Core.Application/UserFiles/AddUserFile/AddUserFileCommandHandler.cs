﻿using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.StorageService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.UserFiles.AddUserFile
{
    public class AddUserFileCommandHandler : BaseHandler, IRequestHandler<AddUserFileCommand, string>
    {
        private readonly IStorageFileService _storageFileService;

        public AddUserFileCommandHandler(ICommonServiceProvider commonServiceProvider, 
            IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _storageFileService = storageFileService;
        }

        public async Task<string> Handle(AddUserFileCommand request, CancellationToken cancellationToken)
        {
            var fileId = await _storageFileService.AddFile(request.Data.File);

            var userFile = new UserFile(request.Data.UserId, fileId);

            await AppDbContext.UserFiles.AddAsync(userFile);
            await AppDbContext.SaveChangesAsync();

            return fileId;
        }
       
    }
}
