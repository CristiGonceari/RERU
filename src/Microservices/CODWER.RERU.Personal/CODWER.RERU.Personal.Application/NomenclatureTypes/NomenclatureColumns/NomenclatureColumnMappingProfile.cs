using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

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
