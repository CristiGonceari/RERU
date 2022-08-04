namespace CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions
{
    public class GetSolicitedVacantPositionDto
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public int RequiredDocumentId { get; set; }
        public string RequiredDocumentName { get; set; }
    }
}
