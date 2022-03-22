using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.Documents
{
    public class DocumentTemplateCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<DocumentTemplateKeyDto> DocumentTemplateKeys { get; set; }
    }
}
