using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.AddNomenclatures
{
    public class AddNomenclatureCommand : IRequest<int>
    {
        public AddEditNomenclatureTypeDto Data { get; set; }

        public AddNomenclatureCommand(AddEditNomenclatureTypeDto data)
        {
            Data = data;
        }
    }
}
