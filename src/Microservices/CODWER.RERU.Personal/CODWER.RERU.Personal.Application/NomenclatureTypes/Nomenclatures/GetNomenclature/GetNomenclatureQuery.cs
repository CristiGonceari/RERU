using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclature
{
    public class GetNomenclatureQuery : IRequest<NomenclatureTypeDto>
    {
        public int Id { get; set; }
    }
}
