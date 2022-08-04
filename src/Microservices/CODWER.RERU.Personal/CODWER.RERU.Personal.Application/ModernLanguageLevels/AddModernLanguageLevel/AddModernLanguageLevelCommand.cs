using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using MediatR;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.AddModernLanguageLevel
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
