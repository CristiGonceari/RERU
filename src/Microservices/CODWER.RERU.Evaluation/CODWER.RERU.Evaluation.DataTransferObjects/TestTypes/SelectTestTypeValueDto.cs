namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTypes
{
    public class SelectTestTypeValueDto
    {
        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; }
        public bool IsOnlyOneAnswer { get; set; }
        public bool PrintTest { get; set; }
    }
}
