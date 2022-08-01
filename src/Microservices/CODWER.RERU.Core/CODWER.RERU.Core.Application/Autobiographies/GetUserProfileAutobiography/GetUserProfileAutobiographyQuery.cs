using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using MediatR;

namespace CODWER.RERU.Core.Application.Autobiographies.GetUserProfileAutobiography
{
    public class GetUserProfileAutobiographyQuery : IRequest<AutobiographyDto>
    {
        public int ContractorId { get; set; }
    }
}
