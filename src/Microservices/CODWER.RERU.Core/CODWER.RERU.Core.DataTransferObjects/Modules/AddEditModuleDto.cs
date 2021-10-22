namespace CODWER.RERU.Core.DataTransferObjects.Modules {
    public class AddEditModuleDto {
        public int Id { set; get; }
        public string Icon { set; get; }
        public string InternalGatewayAPIPath { set; get; }
        public string PublicUrl { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Color { set; get; }
        public int Priority { set; get; }
    }
}