using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal
{
    public class DismissalRequestProfile : Profile
    {
        public DismissalRequestProfile()
        {
            CreateMap<Data.Entities.ContractorEvents.DismissalRequest, MyDismissalRequestDto>()
                .ForMember(x => x.PositionOrganizationRoleName, opts => opts.MapFrom(x => x.Position.OrganizationRole.Name));

            CreateMap<Data.Entities.ContractorEvents.DismissalRequest, DismissalRequestDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(x => x.Contractor.FirstName))
                .ForMember(x => x.ContractorLastName, opts => opts.MapFrom(x => x.Contractor.LastName))

                .ForMember(x => x.PositionOrganizationRoleName, opts => opts.MapFrom(x => x.Position.OrganizationRole.Name));
        }
    }
}
