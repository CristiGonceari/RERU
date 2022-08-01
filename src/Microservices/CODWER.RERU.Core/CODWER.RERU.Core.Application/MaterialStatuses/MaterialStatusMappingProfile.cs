using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.MaterialStatus;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.MaterialStatuses
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
