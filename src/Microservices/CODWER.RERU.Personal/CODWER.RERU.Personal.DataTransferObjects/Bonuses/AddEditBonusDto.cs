namespace CODWER.RERU.Personal.DataTransferObjects.Bonuses
{
    public class AddEditBonusDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public int ContractorId { get; set; }
    }
}
