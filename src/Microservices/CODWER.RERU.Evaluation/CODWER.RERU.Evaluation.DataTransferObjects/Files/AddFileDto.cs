using CODWER.RERU.Evaluation.Data.Entities.Files;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Files
{
    public class AddFileDto
    {
        public IFormFile File { get; set; }
        public FileTypeEnum Type { get; set; }
    }
}
