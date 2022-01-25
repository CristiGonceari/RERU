using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.UpdateNomenclatures
{
    public class UpdateNomenclatureCommand : IRequest<Unit>
    {
        public AddEditNomenclatureTypeDto Data { get; set; }

        public UpdateNomenclatureCommand(AddEditNomenclatureTypeDto data)
        {
            Data = data;
        }
    }
}
