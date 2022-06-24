using MediatR;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.RemoveUserProfileModernLanguageLevel
{
    public class RemoveUserProfileModernLanguageLevelCommand : IRequest<Unit>
    {
        public int ModernLanguageLevelId { get; set; }
    }
}
