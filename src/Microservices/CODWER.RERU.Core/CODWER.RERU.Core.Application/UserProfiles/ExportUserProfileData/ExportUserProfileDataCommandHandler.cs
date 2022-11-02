using CODWER.RERU.Core.Application.Services;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.ExportUserProfileData
{
    public class ExportUserProfileDataCommandHandler : IRequestHandler<ExportUserProfileDataCommand, FileDataDto>
    {
        private readonly IExportUserProfileData _exportUserProfileData;
        public ExportUserProfileDataCommandHandler(IExportUserProfileData exportUserProfileData)
        {
            _exportUserProfileData = exportUserProfileData;
        }

        public Task<FileDataDto> Handle(ExportUserProfileDataCommand request, CancellationToken cancellationToken)
        {
            var data = _exportUserProfileData.ExportUserProfileDatas(request.userProfileId);

            return data;
        }
    }
}
