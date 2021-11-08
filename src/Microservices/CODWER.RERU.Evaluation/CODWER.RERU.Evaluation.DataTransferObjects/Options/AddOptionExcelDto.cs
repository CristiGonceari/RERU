namespace CODWER.RERU.Evaluation.DataTransferObjects.Options
{
    public class AddOptionExcelDto
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public AddEditOptionDto OptionDto { get; set; }
        public bool Error { get; set; }
    }
}
