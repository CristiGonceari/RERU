using AutoMapper;
using CODWER.RERU.Personal.Application.Contractors.ContractorMappings;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using RERU.Data.Entities.PersonalEntities.TimeSheetTables;

namespace CODWER.RERU.Personal.Application.TimeSheetTables
{
    public class TimeSheetTableMappingProfile : Profile
    {
        public TimeSheetTableMappingProfile()
        {
            CreateMap<TimeSheetTableDto, TimeSheetTable>()
                .ForMember(x => x.Date, 
                    opts => opts.MapFrom(op => op.Date.Date));

            CreateMap<AddEditTimeSheetTableDto, TimeSheetTable>()
                .ForMember(x=>x.Value, opts=>opts.MapFrom(op => (TimeSheetValueEnum?)op.ValueId));

            CreateMap<TimeSheetTable, TimeSheetTableDto>()
                .ForMember(x => x.ValueId, 
                    opts => opts.MapFrom(x => MapTimeSheetIntValue(x.Value)))
                .ForMember(x => x.Value, 
                    opts => opts.MapFrom(x => MapTimeSheetStringValue(x.Value)));

            CreateMap<Contractor, ContractorTimeSheetTableDto>()
                .ForMember(x => x.ContractorId, 
                    opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.ContractorName, 
                    opts => opts.MapFrom(op => $"{op.FirstName} {op.LastName} {op.FatherName}"))
                .ForMember(x => x.Content,
                    opts => opts.MapFrom(x => x.TimeSheetTables))
                .ForMember(x => x.Department, 
                    opts => opts.ConvertUsing(new DepartmentNameConverter(), op => op))
                .ForMember(x => x.Role,
                    opts => opts.ConvertUsing(new RoleConverter(), op => op));

            CreateMap<Contractor, ContractorProfileTimeSheetTableDto>()
                .ForMember(x => x.ContractorName,
                    opts => opts.MapFrom(op => $"{op.FirstName} {op.LastName}"))
                .ForMember(x => x.Content, 
                    opts => opts.MapFrom(x => x.TimeSheetTables));
        }

        private int MapTimeSheetIntValue(TimeSheetValueEnum? item)
        {
            if (item != null)
            {
                return (int)item.Value;
            }

            return 0;
        }

        private string MapTimeSheetStringValue(TimeSheetValueEnum? item)
        {
            if (item != null)
            {
                return (int) item.Value < 100 ? ((int) item.Value).ToString() : item.Value.ToString();
            }

            return "";
        }
    }
}
