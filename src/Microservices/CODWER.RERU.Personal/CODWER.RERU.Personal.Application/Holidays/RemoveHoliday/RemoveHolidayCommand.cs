using MediatR;

namespace CODWER.RERU.Personal.Application.Holidays.RemoveHoliday
{
    public class RemoveHolidayCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
