using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using MediatR;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.AddModernLanguageLevel
{
    public class AddModernLanguageLevelCommand : IRequest<int>
    {
        public AddModernLanguageLevelCommand(AddEditModernLanguageLevelDto data)
        {
            Data = data;
        }

        public AddEditModernLanguageLevelDto Data { get; set; }
    }
}
