namespace CVU.ERP.Common.DataTransferObjects.Files
{
    public class FileDataDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
