using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.GetPdfServices
{
    public interface IGetTestTemplatePdf
    {
        public Task<FileDataDto> PrintTestTemplatePdf(int testTemplateId);
    }
}
