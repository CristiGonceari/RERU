using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using MediatR;

namespace CODWER.RERU.Personal.Application.Autobiographies.UpdateAutobiography
{
    public class UpdateAutobiographyCommand : IRequest<Unit>
    {
        public UpdateAutobiographyCommand(AutobiographyDto data)
        {
            Data = data;
        }

        public AutobiographyDto Data { get; set; }
    }
}
