namespace CODWER.RERU.Personal.Application.Validation
{
    public static class ValidationCodes
    {

        //common
        public static string INVALID_INPUT = "02000001";
        public static string NULL_OR_EMPTY_INPUT = "02000002";
        public static string EXISTENT_RECORD = "02000003";
        public static string INVALID_NAME = "02000004";
        public static string INVALID_ID = "02000005";

        //contractor
        public static string CONTRACTOR_NOT_FOUND = "02000101";

        //contractor-type
        public static string CONTRACTOR_TYPE_NOT_FOUND = "02000201";

        //blood type
        public static string BLOOD_TYPE_NOT_FOUND = "02000301";

        //language
        public static string LANGUAGE_NOT_FOUND = "02000401";

        //language level
        public static string LANGUAGE_LEVEL_NOT_FOUND = "02000501";

        //nationality
        public static string NATIONALITY_NOT_FOUND = "02000601";

        //driver license category
        public static string DRIVER_LICENSE_CATEGORY_NOT_FOUND = "02000701";

        //nomenclature type 
        public static string NOMENCLATURE_TYPE_NOT_FOUND = "02000801";

        //contractor type custom field 
        public static string CONTRACTOR_TYPE_CUSTOM_FIELD_NOT_FOUND = "02000901";

        //address 
        public static string ADDRESS_NOT_FOUND = "02001001";
        public static string EMPTY_ADDRESS = "02001002";

        //contractor type custom field value
        public static string CONTRACTOR_TYPE_CUSTOM_FIELD_VALUE_NOT_FOUND = "02001101";

        //contractor languages
        public static string CONTRACTOR_LANGUAGE_NOT_FOUND = "02001201";

        //contractor driver license category
        public static string CONTRACTOR_DRIVER_LICENSE_CATEGORY_NOT_FOUND = "02001301";

        //departments
        public static string DEPARTMENT_NOT_FOUND = "02001401";

        //contractor departments
        public static string CONTRACTOR_DEPARTMENT_NOT_FOUND = "02001501";

        //contractor events
        public static string CONTRACTOR_EVENT_NOT_FOUND = "02001601";

        //attestations
        public static string ATTESTATION_NOT_FOUND = "02001701";

        //badges
        public static string BADGE_NOT_FOUND = "02001801";

        //bonuses
        public static string BONUS_NOT_FOUND = "02001901";

        //penalizations
        public static string PENALIZATION_NOT_FOUND = "02002001";

        //positions
        public static string POSITION_NOT_FOUND = "02002101";

        //ranks
        public static string RANK_NOT_FOUND = "02002201";

        //vacations
        public static string VACATION_NOT_FOUND = "02002301";

        //contacts
        public static string CONTACT_NOT_FOUND = "02002401";
        public static string EXISTENT_CONTACT = "02002402";

        //organization roles
        public static string ORGANIZATION_ROLE_NOT_FOUND = "02002501";

        //organization roles relations
        public static string ORGANIZATION_ROLE_RELATION_NOT_FOUND = "02002601";

        //organizational chart 
        public static string ORGANIZATIONAL_CHART_NOT_FOUND = "02002701";

        //vacation types
        public static string VACATION_TYPES_NOT_FOUND = "02002801";

        //user profile
        public static string NONEXISTENT_USER_PROFILE = "02002901";
        public static string NONEXISTENT_USER_PROFILE_CONTRACTOR = "02002902";

        //files
        public static string INVALID_FILE_NAME = "02003001";
        public static string INVALID_FILE_EXTENSION = "02003002";
        public static string FILE_NOT_FOUND = "02003003";
        public static string FILE_NAME_IS_NOT_UNIQUE = "02003004";
        public static string FILE_IS_CORRUPTED = "02003005";
        public static string INVALID_FILE_TYPE = "02003006";


        //department role relations
        public static string DEPARTMENT_ROLE_CONTENT_NOT_FOUND = "02003101";

        //holidays
        public static string HOLIDAY_NOT_FOUND = "02003201";



        //fieldValues
        public static string INVALID_TEXT_VALUE = "02005001";
        public static string INVALID_CHARACTER_VALUE = "02005002";
        public static string INVALID_INTEGER_VALUE = "02005003";
        public static string INVALID_DOUBLE_VALUE = "02005004";
        public static string INVALID_BOOLEAN_VALUE = "02005005";
        public static string INVALID_DATE_VALUE = "02005006";
        public static string INVALID_DATETIME_VALUE = "02005007";
        public static string INVALID_EMAIL_VALUE = "02005008";

        //nomenlcature columns
        public static string NOMENCLATURE_COLUMN_NOT_FOUND = "02006001";
        public static string NOMENCLATURE_RECORD_NOT_FOUND = "02006002";
        public static string NOMENCLATURE_RECORD_VALUE_NOT_FOUND = "02006003";
        public static string INVALID_COLUMN_TYPE = "02006004";
        public static string INVALID_COLUMN_ORDER = "02006005";
        public static string INVALID_COLUMN_DATA = "02006006";

        //Bulletin
        public static string VALIDATION_IDNP_SHOULD_BE_13_DIGITS = "02007001";
        public static string VALIDATION_IDNP_SHOULD_BE_ONLY_DIGITS = "02007002";
        public static string VALIDATION_IDNP_SHOULD_NOT_BE_ZEROES = "02007003";
        public static string VALIDATION_IDNP_INVALID_CONTROL_DIGIT = "02007004";

        public static string EMPTY_BULLETIN_SERIES = "02007005";
        public static string EMPTY_BULLETIN_EMITTER = "02007006";
        public static string BULLETIN_NOT_FOUND = "02007007";
        public static string CONTRACTOR_HAS_BULLETIN = "02007008";
        public static string DUPLICATE_IDNP_IN_SYSTEM = "02007009";


        //studies
        public static string EMPTY_STUDY_INSTITUTION = "02008001";
        public static string EMPTY_STUDY_FACULTY = "02008002";
        public static string EMPTY_STUDY_INSTITUTION_ADDRESS = "02008003";
        public static string EMPTY_STUDY_SPECIALITY = "02008004";
        public static string EMPTY_STUDY_YEAR_OF_AMISION = "02008005";
        public static string EMPTY_STUDY_GRADUATION_YEAR = "02008006";
        public static string INVALID_STUDY_TYPE = "02008007";
        public static string STUDY_NOT_FOUND = "02008008";

        //contract
        public static string CONTRACT_NOT_FOUND = "02009008";

        public static string INSTRUCTION_NOT_FOUND = "02010001";

        //dismiss
        public static string DISMISS_REQUEST_NOT_FOUND = "02011001";

        //family components
        public static string FAMILY_MEMBER_NOT_FOUND = "02012001";

        //permission 
        public static string LOCAL_PERMISSION_NOT_FOUND = "02013001";

        //pontaj data 
        public static string TIME_SHEET_TABLE_NOT_FOUND = "02014001";

        //documents
        public static string DOCUMENTS_NOT_FOUND = "02015001";

        //article
        public static string EMPTY_CONTENT = "02016001";
        public static string EMPTY_NAME = "02016002";
        public static string ARTICLE_NOT_FOUND = "02016003";

        //RegistrationFluxStep
        public static string REGISTRATION_FLUX_NOT_FOUND = "02017001";

        //ModernLanguage
        public static string MODERN_LANGUAGE_NOT_FOUND = "02018001";

        //RecomandationForStudy
        public static string EMPTY_RECOMANDATION_NAME = "02019001";
        public static string EMPTY_RECOMANDATION_LAST_NAME = "02019002";
        public static string EMPTY_RECOMANDATION_FUNCTION = "01019003";
        public static string EMPTY_RECOMANDATION_SUBDIVISION = "01019004";
        public static string RECOMANDATION_NOT_FOUND = "01019005";

        //MaterialStatus
        public static string MATERIAL_STATUS_NOT_FOUND = "02020001";
        public static string MATERIAL_STATUS_TYPE_NOT_FOUND = "01020002";

        //KinshipRelation
        public static string EMPTY_KINSHIP_RELATION_NAME = "02021001";
        public static string EMPTY_KINSHIP_RELATION_LAST_NAME = "02021002";
        public static string EMPTY_KINSHIP_RELATION_FUNCTION = "02021003";
        public static string EMPTY_KINSHIP_RELATION_SUBDIVISION = "02021004";
        public static string KINSHIP_RELATION_NOT_FOUND = "02021005";
        public static string EMPTY_KINSHIP_RELATION_BIRTH_LOCATION = "02021006";
        public static string EMPTY_KINSHIP_RELATION_WORK_LOCATION = "02021007";
        public static string EMPTY_KINSHIP_RELATION_RESIDENCE_ADDRESS = "02021008";
        public static string EXISTENT_KINSHIP_RELATION_IN_SISTEM = "02021009";

        //Autobiography 
        public static string EMPTY_AUTOBIOGRAPHY_TEXT = "02022001";
        public static string AUTOBIOGRAPHY_NOT_FOUND = "02022002";

        //MilitaryObligation
        public static string EMPTY_MILITARY_OBLIGATION_EFECTIVE = "02023001";
        public static string EMPTY_MILITARY_OBLIGATION_SPECIALITY = "02023001";
        public static string EMPTY_MILITARY_OBLIGATION_DEGREE = "02023001";
        public static string EMPTY_MILITARY_BOOKLET_SERIES = "02023001";
        public static string EMPTY_MILITARY_BOOKLET_NUMBER = "02023001";
        public static string EMPTY_MILITARY_BOOKLET_AUTHORITY = "02023001";
        public static string MILITARY_OBLIGATION_NOT_FOUND = "02023001";
    }


}
