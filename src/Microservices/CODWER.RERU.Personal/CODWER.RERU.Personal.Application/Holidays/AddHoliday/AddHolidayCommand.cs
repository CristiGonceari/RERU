using CODWER.RERU.Personal.DataTransferObjects.Holidays;
using MediatR;

namespace CODWER.RERU.Personal.Application.Holidays.AddHoliday
{
    public class AddHolidayCommand : IRequest<int>
    {
        public AddEditHolidayDto Data { get; set; }
    }
}
