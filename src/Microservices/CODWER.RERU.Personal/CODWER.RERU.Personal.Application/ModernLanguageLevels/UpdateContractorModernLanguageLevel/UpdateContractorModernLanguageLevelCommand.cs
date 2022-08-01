using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using MediatR;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.UpdateContractorModernLanguageLevel
{
    public class UpdateContractorModernLanguageLevelCommand : IRequest<Unit>
    {
        public UpdateContractorModernLanguageLevelCommand(AddEditModernLanguageLevelDto dto)
        {
            Data = dto;
        }

        public AddEditModernLanguageLevelDto Data { get; set; }
    }
}
