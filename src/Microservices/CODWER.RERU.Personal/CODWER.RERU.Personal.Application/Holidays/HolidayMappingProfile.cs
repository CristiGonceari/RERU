using System;
using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.DataTransferObjects.Holidays;

namespace CODWER.RERU.Personal.Application.Holidays
{
    public class HolidayMappingProfile : Profile
    {
        public HolidayMappingProfile()
        {
            CreateMap<HolidayDto, Holiday>();

            CreateMap<Holiday, HolidayDto>();

            CreateMap<AddEditHolidayDto, Holiday>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.From, opts => opts.MapFrom(op => op.From.Date))
                .ForMember(x => x.To, opts => opts.MapFrom(op => MapToDate(op.To)));
        }

        private DateTime? MapToDate(DateTime? to)
        {
            return to?.Date;
        }
    }
}
