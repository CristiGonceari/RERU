namespace CVU.ERP.Common.DataTransferObjects.Files
{
    public class FileDataDto
    {
        public FileDataDto(string name, byte[] content, string contentType)
        {
            Name = name;
            Content = content;
            ContentType = contentType;
        }

        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public static FileDataDto GetPdf(string name, byte[] content)
        {
            return new FileDataDto(name, content, "application/pdf");
        }

        public static FileDataDto GetExcel(string name, byte[] content)
        {
            return new FileDataDto(name, content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public static FileDataDto GetXml(string name, byte[] content)
        {
            return new FileDataDto(name, content, "application/xml");
        }

        public static FileDataDto GetByType(string name, byte[] content, string contentType)
        {
            return new FileDataDto(name, content, contentType);
        }
    }
}
