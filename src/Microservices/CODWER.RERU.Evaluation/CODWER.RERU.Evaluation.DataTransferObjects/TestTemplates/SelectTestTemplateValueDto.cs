namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates
{
    public class SelectTestTemplateValueDto
    {
        public int TestTypeId { get; set; }
        public string TestTypeName { get; set; }
        public bool IsOnlyOneAnswer { get; set; }
        public bool PrintTest { get; set; }
    }
}
