using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Evaluation360.DataTransferObjects.Articles
{
    public class ArticleEv360Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public AddFileDto? FileDto { get; set; }
        public string MediaFileId { get; set; }
        public bool ContainsMedia { get; set; }
        public List<SelectItem> Roles { get; set; }
    }
}
