using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.GetUserProfileModernLanguageLevels
{
    public class GetUserProfileModernLanguageLevelsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModernLanguageLevelDto>>
    {
        public int ContractorId { get; set; }
    }
}
