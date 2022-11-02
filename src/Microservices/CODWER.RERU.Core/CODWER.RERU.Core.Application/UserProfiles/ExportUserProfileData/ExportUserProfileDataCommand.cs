using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.ExportUserProfileData
{
    public class ExportUserProfileDataCommand : IRequest<FileDataDto>
    {
        public int userProfileId { get; set; }
    }

}
