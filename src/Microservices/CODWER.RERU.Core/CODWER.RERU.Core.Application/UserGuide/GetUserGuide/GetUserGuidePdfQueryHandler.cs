using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserGuide.GetUserGuide
{
    public class GetUserGuidePdfQueryHandler : IRequestHandler<GetUserGuidePdfQuery, FileDataDto>
    {
        public async Task<FileDataDto> Handle(GetUserGuidePdfQuery request, CancellationToken cancellationToken)
        {
            var path = new FileInfo("UserGuide/GhidCore.pdf").FullName;
            var bytes = await File.ReadAllBytesAsync(path);

            return FileDataDto.GetPdf("Ghidul Utilizatorului.pdf", bytes);
        }
    }
}
