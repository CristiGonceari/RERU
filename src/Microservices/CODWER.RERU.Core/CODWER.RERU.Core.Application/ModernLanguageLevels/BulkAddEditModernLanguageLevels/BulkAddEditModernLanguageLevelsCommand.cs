using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.BulkAddEditModernLanguageLevels
{
    public class BulkAddEditModernLanguageLevelsCommand : IRequest<Unit>
    {
        public BulkAddEditModernLanguageLevelsCommand(List<AddEditModernLanguageLevelDto> list)
        {
            Data = list;
        }

        public List<AddEditModernLanguageLevelDto> Data { get; set; }
    }
}
