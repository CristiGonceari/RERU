using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles.BulkAddEditKinshipRelationWithUserProfiles
{
    public class BulkAddEditKinshipRelationWithUserProfilesCommand : IRequest<Unit>
    {
        public BulkAddEditKinshipRelationWithUserProfilesCommand(List<KinshipRelationWithUserProfileDto> list)
        {
            Data = list;
        }

        public List<KinshipRelationWithUserProfileDto> Data { get; set; }
    }
}
