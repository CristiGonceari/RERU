namespace CVU.ERP.Module.API.Middlewares.ResponseWrapper {
    public class ResponseWrapperOptions {
        public bool IsDebug { get; set; } = false;

        /// <summary>
        /// Shows the IsError attribute in the response.
        /// </summary>
        public bool ShowIsErrorFlagForSuccessfulResponse { get; set; } = false;

        /// <summary>
        /// Use to indicate if the wrapper is used for API project only. 
        /// Set this to false when you want to use the wrapper within an Angular, MVC, React or Blazor projects.
        /// </summary>
        public bool IsApiOnly { get; set; } = true;

        /// <summary>
        /// Tells the wrapper to ignore validation for string that contains HTML
        /// </summary>
        public bool BypassHTMLValidation { get; set; } = false;

        /// <summary>
        /// Set the Api path segment to validate. The default value is '/api'. Only works if IsApiOnly is set to false.
        /// </summary>
        public string[] WrapWhenApiPathStartsWith { get; set; } = new string[] { "/api", "/internal/api" };

        /// <summary>
        /// Tells the wrapper to ignore attributes with null values. Default is true.
        /// </summary>
        public bool IgnoreNullValue { get; set; } = true;

        public string SwaggerPath { get; set; } = "/swagger";
    }
}