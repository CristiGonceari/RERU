using System;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Vacations.MyVacations.GetVacationDaysInterval
{
    public class GetVacationDaysIntervalQuery : IRequest<int>
    {
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
    }
}
