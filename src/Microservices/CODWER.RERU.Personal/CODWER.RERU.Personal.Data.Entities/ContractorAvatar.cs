namespace CODWER.RERU.Personal.Data.Entities
{
    public class ContractorAvatar
    {
        public int Id { get; set; }
        public string AvatarBase64 { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
