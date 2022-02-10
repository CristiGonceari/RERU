using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.ContractorEvents;
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

                .ForMember(x => x.PositionOrganizationRoleName, opts => opts.MapFrom(x => x.Position.OrganizationRole.Name));
        }
    }
}
