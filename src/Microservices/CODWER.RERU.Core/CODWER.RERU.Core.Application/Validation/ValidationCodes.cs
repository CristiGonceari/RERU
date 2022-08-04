
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
        public static string DUPLICATE_EMAIL_IN_SYSTEM = "01000107";
        public static string INVALID_CODE = "01000108";
        public static string INVALID_USER_BIRTH_DATE = "01000109";
        public static string EMPTY_USER_WORK_PHONE = "01000110";
        public static string EMPTY_USER_HOME_PHONE = "01000111";
        public static string EMPTY_USER_MOBILE_PHONE = "01000112";
        public static string USER_NOT_FOUND = "01000113";
        public static string NULL_OR_EMPTY_INPUT = "01000114";
        public static string INVALID_USER_PHONE = "01000115";

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

        //ROLES
        public static string INVALID_ROLE_ID = "01000501";

        //DEPARTMENT
        public static string INVALID_DEPARTMENT_ID = "01000602";

        //Bulletin
        public static string EMPTY_BULLETIN_SERIES = "01000701";
        public static string EMPTY_BULLETIN_EMITTER = "01000702";
        public static string BULLETIN_NOT_FOUND = "01000703";
        public static string CONTRACTOR_HAS_BULLETIN = "01000704";

        //Address 
        public static string ADDRESS_NOT_FOUND = "01000801";
        public static string EMPTY_ADDRESS = "01000802";

        //Studies
        public static string EMPTY_STUDY_INSTITUTION = "01000901";
        public static string EMPTY_STUDY_FACULTY = "01000902";
        public static string EMPTY_STUDY_INSTITUTION_ADDRESS = "01000903";
        public static string EMPTY_STUDY_SPECIALITY = "01000904";
        public static string EMPTY_STUDY_YEAR_OF_AMISION = "01000905";
        public static string EMPTY_STUDY_GRADUATION_YEAR = "01000906";
        public static string INVALID_STUDY_TYPE = "01000907";
        public static string STUDY_NOT_FOUND = "01000908";

        //ModernLanguage
        public static string MODERN_LANGUAGE_NOT_FOUND = "01001001";

        //RecomandationForStudy
        public static string EMPTY_RECOMANDATION_NAME = "01002001";
        public static string EMPTY_RECOMANDATION_LAST_NAME = "01002002";
        public static string EMPTY_RECOMANDATION_FUNCTION = "01002003";
        public static string EMPTY_RECOMANDATION_SUBDIVISION = "01002004";
        public static string RECOMANDATION_NOT_FOUND = "01002005";


        //MaterialStatus
        public static string MATERIAL_STATUS_NOT_FOUND = "01003001";
        public static string MATERIAL_STATUS_TYPE_NOT_FOUND = "01003002";

        //KinshipRelation
        public static string EMPTY_KINSHIP_RELATION_NAME = "01004001";
        public static string EMPTY_KINSHIP_RELATION_LAST_NAME = "01004002";
        public static string EMPTY_KINSHIP_RELATION_FUNCTION = "01004003";
        public static string EMPTY_KINSHIP_RELATION_SUBDIVISION = "01004004";
        public static string KINSHIP_RELATION_NOT_FOUND = "01004005";
        public static string EMPTY_KINSHIP_RELATION_BIRTH_LOCATION = "01004006";
        public static string EMPTY_KINSHIP_RELATION_WORK_LOCATION = "01004007";
        public static string EMPTY_KINSHIP_RELATION_RESIDENCE_ADDRESS = "01004008";
        public static string EXISTENT_KINSHIP_RELATION_IN_SISTEM = "01004009";

        //Autobiography 
        public static string EMPTY_AUTOBIOGRAPHY_TEXT = "01005001";
        public static string AUTOBIOGRAPHY_NOT_FOUND = "01005001";

        //MilitaryObligation
        public static string EMPTY_MILITARY_OBLIGATION_EFECTIVE = "01006001";
        public static string EMPTY_MILITARY_OBLIGATION_SPECIALITY = "01006002";
        public static string EMPTY_MILITARY_OBLIGATION_DEGREE = "01006003";
        public static string EMPTY_MILITARY_BOOKLET_SERIES = "01006004";
        public static string EMPTY_MILITARY_BOOKLET_NUMBER = "01006005";
        public static string EMPTY_MILITARY_BOOKLET_AUTHORITY = "01006006";
        public static string MILITARY_OBLIGATION_NOT_FOUND = "01006007";

        //RegistrationFluxStep
        public static string REGISTRATION_FLUX_NOT_FOUND = "01007001";

        //GenearalData
        public static string USER_PROFILE_GENERAL_DATA_NOT_FOUND = "01008003";
        public static string CONTRACTOR_HAS_GENERAL_DATA = "01008004";
        public static string CANDIDATE_CITIZENSHIP_NOT_FOUND = "01008001";
        public static string CANDIDATE_NATIONALITY_NOT_FOUND = "01008002";
    }
}
