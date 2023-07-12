using MediatR;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Contractors.GetAllContractorDatas
{
    public class GetAllContractorDataQuery : IRequest<string>
    {
        public int Id { get; set; }
    }
}
