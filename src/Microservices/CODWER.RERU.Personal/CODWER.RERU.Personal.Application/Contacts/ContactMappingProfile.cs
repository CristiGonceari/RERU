using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;

namespace CODWER.RERU.Personal.Application.Contacts
{
    public class ContactMappingProfile : Profile
    {
        public ContactMappingProfile()
        {
            CreateMap<AddEditContactDto, Contact>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Contact, ContactDto>()
                .ForMember(x => x.Type, opts => opts.MapFrom(op => op.Type))
                .ForMember(x => x.TypeName, opts => opts.MapFrom(op => op.Type.ToString()))
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Value))
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(op => op.ContractorId))
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()));

            CreateMap<Contact, ContactRowDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.TypeName, opts => opts.MapFrom(op => op.Type.ToString()))
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Value));

            CreateMap<Contact, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => op.Value));
        }
    }
}
