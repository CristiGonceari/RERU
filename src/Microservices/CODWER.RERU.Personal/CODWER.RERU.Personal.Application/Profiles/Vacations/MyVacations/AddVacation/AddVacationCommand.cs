using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.AddVacation
{
    public class AddVacationCommand : IRequest<int>
    {
        public AddMyVacationDto Data { get; set; }

        public AddVacationCommand(AddMyVacationDto data)
        {
            Data = data;
        }
    }
}
