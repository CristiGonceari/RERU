using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.GetPersonalFile
{
    public class GetPersonalFileQueryHandler : IRequestHandler<GetPersonalFileQuery, FileDataDto>
    {
        public async Task<FileDataDto> Handle(GetPersonalFileQuery request, CancellationToken cancellationToken)
        {
            var path = new FileInfo("PersonalFile/Fisa_Personala.xlsx").FullName;
            var bytes = await File.ReadAllBytesAsync(path);

            return FileDataDto.GetExcel("Fisa Personala.xlsx", bytes);
        }
    }
}
