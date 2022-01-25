using CODWER.RERU.Personal.DataTransferObjects.Holidays;
using MediatR;

namespace CODWER.RERU.Personal.Application.Holidays.UpdateHoliday
{
    public class UpdateHolidayCommand : IRequest<Unit>
    {
        public AddEditHolidayDto Data { get; set; }
    }
}
