using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures
{
    public class NomenclatureMappingProfile : Profile
    {
        public NomenclatureMappingProfile()
        {
            CreateMap<AddEditNomenclatureTypeDto, NomenclatureType>()
                .ForMember(x=>x.Id, opts=>opts.Ignore())
                .ForMember(x=>x.IsActive, opts=>opts.Ignore());

            CreateMap<NomenclatureType, NomenclatureTypeDto>();
            CreateMap<NomenclatureType, AddEditNomenclatureTypeDto>();

            CreateMap<NomenclatureType, SelectItem>()
                .ForMember(x => x.Label, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.Id));
        }
    }
}
