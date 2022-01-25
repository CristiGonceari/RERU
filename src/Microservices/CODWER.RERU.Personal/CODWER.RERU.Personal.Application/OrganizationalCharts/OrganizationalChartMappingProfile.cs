using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
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
