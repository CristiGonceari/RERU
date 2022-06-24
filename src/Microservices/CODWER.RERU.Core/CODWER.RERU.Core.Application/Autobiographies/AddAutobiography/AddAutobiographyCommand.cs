using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using MediatR;

namespace CODWER.RERU.Core.Application.Autobiographies.AddAutobiography
{
    public class AddAutobiographyCommand : IRequest<int>
    {
        public AddAutobiographyCommand(AutobiographyDto data)
        {
            Data = data;
        }

        public AutobiographyDto Data { get; set; }
    }
}
