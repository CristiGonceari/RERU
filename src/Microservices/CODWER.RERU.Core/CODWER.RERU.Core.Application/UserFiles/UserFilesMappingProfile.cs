using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.StorageService.Entities;

namespace CODWER.RERU.Core.Application.UserFiles
{
    public class UserFilesMappingProfile : Profile
    {
        public UserFilesMappingProfile()
        {
            CreateMap<File, GetFilesDto>()
                .ForMember(x => x.FileId, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => $"{op.FileName}"));
        }
    }
}
