namespace CODWER.RERU.Personal.Data.Entities
{
    public class ContractorAvatar
    {
        public int Id { get; set; }
        public string? MediaFileId { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
