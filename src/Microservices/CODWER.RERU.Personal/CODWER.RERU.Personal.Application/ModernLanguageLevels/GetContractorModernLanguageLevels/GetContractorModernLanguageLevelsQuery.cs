using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.GetContractorModernLanguageLevels
{
    public class GetContractorModernLanguageLevelsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModernLanguageLevelDto>>
    {
        public int ContractorId { get; set; }

    }
}
