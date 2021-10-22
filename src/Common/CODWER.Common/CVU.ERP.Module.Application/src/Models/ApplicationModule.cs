namespace CVU.ERP.Module.Application.Models {
    ///<summary>
    /// This class is used to represent the module agains entire application
    ///</summary>
    public class ApplicationModule {
        public string Name { set; get; }
        public string Code { set; get; }
        public string Icon { set; get; }
        public string PublicUrl { set; get; }
        public string InternalGatewayAPIPath { set; get; }
    }
}