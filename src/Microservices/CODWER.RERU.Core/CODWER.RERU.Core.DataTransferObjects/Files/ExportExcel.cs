namespace CODWER.RERU.Core.DataTransferObjects.Files
{
    public class ExportExcel
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
