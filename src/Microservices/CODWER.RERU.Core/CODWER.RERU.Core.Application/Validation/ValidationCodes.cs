
namespace CODWER.RERU.Core.Application.Validation
{
    public static class ValidationCodes
    {
        //IDNP
        public static string VALIDATION_IDNP_SHOULD_BE_13_DIGITS = "01000001";
        public static string VALIDATION_IDNP_SHOULD_BE_ONLY_DIGITS = "01000002";
        public static string VALIDATION_IDNP_SHOULD_NOT_BE_ZEROES = "01000003";
        public static string VALIDATION_IDNP_INVALID_CONTROL_DIGIT = "01000004";
        public static string DUPLICATE_IDNP_IN_SYSTEM = "01000005";
         
        //USER
        public static string EMPTY_USER_NAME = "01000101";
        public static string EMPTY_USER_LAST_NAME = "01000102";
        public static string EMPTY_USER_FATHER_NAME = "01000103";
        public static string EMPTY_USER_EMAIL = "01000104";

    }
}
