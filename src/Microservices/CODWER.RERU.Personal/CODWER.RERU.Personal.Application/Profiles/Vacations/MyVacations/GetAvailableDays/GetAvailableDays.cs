using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetAvailableDays
{
    public class GetAvailableDays : IRequest<double>
    {
        public int VacantionTypeId { get; set; }
    }
}
