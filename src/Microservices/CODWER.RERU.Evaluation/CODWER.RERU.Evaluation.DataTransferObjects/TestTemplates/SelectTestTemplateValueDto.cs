namespace CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates
{
    public class SelectTestTemplateValueDto
    {
        public int TestTemplateId { get; set; }
        public string TestTemplateName { get; set; }
        public bool IsOnlyOneAnswer { get; set; }
        public bool PrintTest { get; set; }
    }
}
