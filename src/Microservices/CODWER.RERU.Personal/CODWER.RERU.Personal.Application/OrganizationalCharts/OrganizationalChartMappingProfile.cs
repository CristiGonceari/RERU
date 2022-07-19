using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts
{
    public class OrganizationalChartMappingProfile : Profile
    {
        public OrganizationalChartMappingProfile()
        {
            CreateMap<AddEditOrganizationalChartDto, OrganizationalChart>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                ;

            CreateMap<OrganizationalChart, OrganizationalChartDto>();
        }
    }
}
