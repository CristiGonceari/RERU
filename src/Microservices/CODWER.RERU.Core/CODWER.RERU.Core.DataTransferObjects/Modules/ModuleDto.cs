namespace CODWER.RERU.Core.DataTransferObjects.Modules {
    public class ModuleDto 
    {
        public int Id { set; get; }
        public string Icon { set; get; }
        public int Type { set; get; }
        public string InternalGatewayAPIPath { set; get; }
        public string PublicUrl { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public int Priority { set; get; }
        public string Color { set; get; }
        public bool Status { set; get; }
    }
}