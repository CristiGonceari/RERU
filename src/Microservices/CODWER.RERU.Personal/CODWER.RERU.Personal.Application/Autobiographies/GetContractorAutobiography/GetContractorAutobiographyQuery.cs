using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using MediatR;

namespace CODWER.RERU.Personal.Application.Autobiographies.GetContractorAutobiography
{
    public class GetContractorAutobiographyQuery : IRequest<AutobiographyDto>
    {
        public int ContractorId { get; set; }
    }
}
