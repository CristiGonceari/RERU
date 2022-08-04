using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.MaterialStatus;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.MaterialStatuses
{
    public class MaterialStatusMappingProfile : Profile
    {
        public MaterialStatusMappingProfile()
        {
            CreateMap<AddEditMaterialStatusDto, MaterialStatus>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<MaterialStatus, AddEditMaterialStatusDto>();
        }
    }
}
