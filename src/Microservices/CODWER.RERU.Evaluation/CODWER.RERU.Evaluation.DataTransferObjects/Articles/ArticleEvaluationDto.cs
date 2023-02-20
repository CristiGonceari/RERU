using System.Collections.Generic;
using System.Xml.Serialization;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.StorageService.Models;

namespace CODWER.RERU.Evaluation.DataTransferObjects.Articles
{
    public class ArticleEvaluationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        [XmlIgnore]
        public AddFileDto? FileDto { get; set; }
        public string MediaFileId { get; set; }
        public bool ContainsMedia { get; set; }
        public List<SelectItem> Roles { get; set; }
    }
}
