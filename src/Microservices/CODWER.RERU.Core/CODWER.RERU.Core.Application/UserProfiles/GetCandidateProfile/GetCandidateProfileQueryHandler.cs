using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.GetCandidateProfile
{
    public class GetCandidateProfileQueryHandler : BaseHandler, IRequestHandler<GetCandidateProfileQuery, CandidateProfileDto>
    {
        public GetCandidateProfileQueryHandler(ICommonServiceProvider commonServiceProvider) : base(commonServiceProvider) { }

        public async Task<CandidateProfileDto> Handle(GetCandidateProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await AppDbContext.UserProfiles
                .Include(up => up.Contractor)
                .ThenInclude(up => up.Bulletin)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.Studies)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.ModernLanguageLevels)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.RecommendationForStudies)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.MaterialStatus)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.KinshipRelations)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.KinshipRelationCriminalData)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.KinshipRelationWithUserProfiles)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.MilitaryObligations)
                .Include(up => up.Contractor)
                .ThenInclude(up => up.Autobiography)
                .FirstOrDefaultAsync(u => u.Id == request.Id);

            var userProfDto = Mapper.Map<CandidateProfileDto>(userProfile);

            if (userProfile.MediaFileId == null) return userProfDto;
           
            return userProfDto;
        }
    }
}
