using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using MediatR;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.UpdateUserProfileModernLanguageLevel
{
    public class UpdateUserProfileModernLanguageLevelCommand : IRequest<Unit>
    {
        public UpdateUserProfileModernLanguageLevelCommand(AddEditModernLanguageLevelDto dto)
        {
            Data = dto;
        }

        public AddEditModernLanguageLevelDto Data { get; set; }
    }
}
