namespace CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles
{
    public class AddEditUserProfileDto
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }
}
