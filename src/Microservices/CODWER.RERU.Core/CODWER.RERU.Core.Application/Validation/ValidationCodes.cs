
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
        public static string EMPTY_USER_ID = "01000105";
        public static string INVALID_EMAIL_FORMAT = "01000105";
        public static string EMPTY_ACCESS_MODE = "01000106";

        //ARTICLES
        public static string EMPTY_NAME = "01000201";
        public static string EMPTY_CONTENT = "01000202";
        public static string INVALID_INPUT = "01000203";
        public static string INVALID_ID = "01000204";

        //CANDIDATE_POSITION
        public static string INVALID_POSITION = "01000301";
        public static string EMPTY_POSITION_NAME = "01000302";

        //FILES
        public static string INVALID_FILE_ID = "01000401";
        public static string FILE_IS_CORRUPTED = "01000402";
    }
}
