namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class EmailVerificationCodeDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
