using CVU.ERP.Common.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Data.Entities.Files
{
    public class File : SoftDeleteBaseEntity
    {
        public new Guid Id { get; set; }
        public string FileName { get; set; }
        public string UniqueFileName { get; set; }
        public string Type { get; set; }
        public FileTypeEnum FileType { get; set; }
        public string BucketName { get; set; }

    }
}
