using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserFiles.PrintUserFiles
{
    public class PrintUserFilesCommand : TableParameter, IRequest<FileDataDto>
    {
        public int UserProfileId { get; set; }
    }
}
