using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Positions;

namespace CODWER.RERU.Personal.Application.Positions
{
    public class PositionMappingProfile : Profile
    {
        public PositionMappingProfile()
        {
            CreateMap<AddEditPositionDto, Position>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.No, opts => opts.Ignore())
                .ForMember(x => x.Role, opts => opts.Ignore())
                .ForMember(x => x.Department, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore());

            CreateMap<Position, PositionDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(op => op.Department.Name))
                .ForMember(x => x.RoleName, opts => opts.MapFrom(op => op.Role.Name));

            CreateMap<AddEditPositionFromContractorDto, Position>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.ToDate, opts => opts.Ignore())
                .ForMember(x => x.ContractorId, opts => opts.Ignore())
                .ForMember(x => x.Department, opts => opts.Ignore())
                .ForMember(x => x.Role, opts => opts.Ignore());

            CreateMap<AddEditCurrentPositionDto, Position>()
                .ForMember(x => x.FromDate, opts => opts.MapFrom(op => op.FromDate))
                .ForMember(x => x.DepartmentId, opts => opts.MapFrom(op => op.DepartmentId))
                .ForMember(x => x.RoleId, opts => opts.MapFrom(op => op.RoleId))
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(op => op.ContractorId));
            ;
                //.ForAllOtherMembers(opts=>opts.Ignore());

            CreateMap<Position, CurrentPositionDto>();

            CreateMap<AddEditPreviousPositionDto, Position>();
        }
    }
}
