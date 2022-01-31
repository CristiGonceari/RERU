using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string CoreUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
