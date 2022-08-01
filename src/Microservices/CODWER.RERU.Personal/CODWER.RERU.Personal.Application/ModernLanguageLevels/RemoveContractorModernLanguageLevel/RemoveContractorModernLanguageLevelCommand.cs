using MediatR;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.RemoveContractorModernLanguageLevel
{
    public class RemoveContractorModernLanguageLevelCommand : IRequest<Unit>
    {
        public int ModernLanguageLevelId { get; set; }
    }
}
