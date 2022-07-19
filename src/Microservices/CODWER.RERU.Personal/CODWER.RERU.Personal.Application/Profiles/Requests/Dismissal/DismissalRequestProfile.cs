using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal
{
    public class DismissalRequestProfile : Profile
    {
        public DismissalRequestProfile()
        {
            CreateMap<DismissalRequest, MyDismissalRequestDto>()
                .ForMember(x => x.PositionOrganizationRoleName, opts => opts.MapFrom(x => x.Position.Role.Name));

            CreateMap<DismissalRequest, DismissalRequestDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(x => x.Contractor.FirstName))
                .ForMember(x => x.ContractorLastName, opts => opts.MapFrom(x => x.Contractor.LastName))
                .ForMember(x => x.PositionRoleName, opts => opts.MapFrom(x => x.Position.Role.Name));
        }
    }
}
