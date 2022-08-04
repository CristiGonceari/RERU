using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateProfile
{
    public class GetCandidateProfileQuery : IRequest<CandidateProfileDto>
    {
        public GetCandidateProfileQuery(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}
