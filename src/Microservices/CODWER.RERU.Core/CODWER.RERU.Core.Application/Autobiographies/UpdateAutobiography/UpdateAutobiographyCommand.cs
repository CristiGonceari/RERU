using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using MediatR;

namespace CODWER.RERU.Core.Application.Autobiographies.UpdateAutobiography
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
