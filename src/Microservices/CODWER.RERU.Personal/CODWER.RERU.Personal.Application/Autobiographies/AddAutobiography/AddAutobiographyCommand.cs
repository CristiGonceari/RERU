using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using MediatR;

namespace CODWER.RERU.Personal.Application.Autobiographies.AddAutobiography
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
