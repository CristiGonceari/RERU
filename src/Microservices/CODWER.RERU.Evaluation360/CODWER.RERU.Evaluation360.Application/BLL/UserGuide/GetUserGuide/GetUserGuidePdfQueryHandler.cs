using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation360.Application.BLL.UserGuide.GetUserGuide
{
    public class GetUserGuidePdfQueryHandler : IRequestHandler<GetUserGuidePdfQuery, FileDataDto>
    {
        public async Task<FileDataDto> Handle(GetUserGuidePdfQuery request, CancellationToken cancellationToken)
        {
            var path = new FileInfo("UserGuide/Ghid360.pdf").FullName;
            var bytes = await File.ReadAllBytesAsync(path);

            return FileDataDto.GetPdf("Ghidul Utilizatorului.pdf", bytes);
        }
    }
}