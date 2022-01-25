using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns
{
    public class NomenclatureColumnMappingProfile : Profile
    {
        public NomenclatureColumnMappingProfile()
        {
            CreateMap<NomenclatureColumn, NomenclatureColumnItemDto>();
        }
    }
}
