using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.RemoveNomenclatures
{
    public class DisableNomenclatureCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
