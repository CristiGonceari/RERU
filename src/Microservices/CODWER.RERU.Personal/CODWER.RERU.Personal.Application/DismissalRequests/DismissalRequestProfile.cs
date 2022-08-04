using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;

namespace CODWER.RERU.Personal.Application.DismissalRequests
{
    public class DismissalRequestProfile: Profile
    {
        public DismissalRequestProfile()
        {
            CreateMap<DismissalRequest, DismissalRequestDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(x => x.Contractor.FirstName))
                .ForMember(x => x.ContractorLastName, opts => opts.MapFrom(x => x.Contractor.LastName))

                .ForMember(x => x.PositionRoleName, opts => opts.MapFrom(x => x.Position.Role.Name))
                ;
        }
    }
}
