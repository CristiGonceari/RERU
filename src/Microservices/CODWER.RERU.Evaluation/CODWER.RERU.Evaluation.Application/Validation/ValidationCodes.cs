namespace CODWER.RERU.Evaluation.Application.Validation
{
    public class ValidationCodes
    {
        //common
        public static string INVALID_INPUT = "03000001";
        public static string NULL_OR_EMPTY_INPUT = "03000002";
        public static string EXISTENT_RECORD = "03000003";
        public static string EMPTY_NAME = "03000004";
        public static string INVALID_ID = "03000005";
        public static string INVALID_RECORD = "03000006";
        public static string INVALID_TIME_RANGE = "03000007";
        public static string USER_ALREADY_ASSIGNED = "03000008";

        //article
        public static string EMPTY_CONTENT = "03000101";

        //events
        public static string INVALID_START_DATE = "03000201";
        public static string INVALID_END_DATE = "03000202";
        public static string INVALID_EVENT = "03000203";
        public static string FINISHED_EVENT = "03000204";
        public static string ONLY_POLLS_OR_TESTS = "03000205";
        public static string INEXISTENT_TEST_TYPE_IN_EVENT = "03000206";
        public static string MUST_ADD_EVENT_OR_EVALUATOR = "03000207";

        //event evaluator
        public static string EXISTENT_EVALUATOR_IN_EVENT = "03000301";
        public static string EXISTENT_EVALUATOR_IN_TEST = "03000302";
        public static string EVALUATOR_AND_CANDIDATE_CANT_BE_THE_SAME = "03000303";

        //event location
        public static string EXISTENT_LOCATION_IN_EVENT = "03000401";

        //event responsible person
        public static string USER_ISNT_RESPONSIBLE_FOR_THIS_EVENT = "03000501";

        //event test type
        public static string EXISTENT_TEST_TYPE_IN_EVENT = "03000601";

        //event user
        public static string INEXISTENT_CANDIDATE_IN_EVENT = "03000701";
        public static string CANDIDATE_AND_RESPONSIBLE_PERSON_CANT_BE_THE_SAME = "03000702";

        //location
        public static string EMPTY_ADDRESS = "03000801";
        public static string EMPTY_COMPUTERS_COUNT = "03000802";
        public static string EMPTY_COMPUTERS_NUMBER = "03000803";
        public static string EMPTY_LOCATION_NAME = "03000804";
        public static string INVALID_LOCATION = "03000805";

        //location client
        public static string INVALID_COMPUTER_NUMBER = "03000901";

        //location responsible person
        public static string EXISTENT_RESPONSIBLE_PERSON_IN_LOCATION = "03001001";
        public static string USER_ISNT_RESPONSIBLE_FOR_THIS_LOCATION = "03001002";

        //options
        public static string EMPTY_ANSWER = "03001101";
        public static string EMPTY_CORRECT_ANSWER = "03001102";

        //plan
        public static string INVALID_PLAN = "03001201";

        //question category
        public static string INVALID_CATEGORY = "03001301";
        public static string EXISTENT_CATEGORY = "03001302";
        public static string EMPTY_CATEGORY_NAME = "03001303";
        public static string CATEGORY_USED_IN_ACTIVE_TESTS_CANT_BE_DELETED = "03001304";

        //question
        public static string EMPTY_QUESTION = "03001401";
        public static string EMPTY_QUESTION_TYPE = "03001402";
        public static string QUESTION_IS_IN_ACTIVE_TEST_TYPE = "03001403";
        public static string EMPTY_FILE = "03001404";
        public static string ONLY_XLSX_FORMAT = "03001405";
        public static string EMPTY_QUESTION_STATUS = "03001406";
        public static string INVALID_QUESTION_POINTS = "03001407";
        public static string INVALID_QUESTION = "03001408";
        public static string INVALID_OPTION = "03001409";
        public static string EMPTY_CORRECT_OPTION = "03001419";
        public static string TAGS_WRITTEN_WITH_MISTAKE_OR_MISSING_ANSWER_OPTION = "03001420";


        //tests
        public static string INVALID_TEST = "03001501";
        public static string ONLY_IN_PROGRESS_TESTS_CAN_BE_TERMINATED = "03001502";
        public static string REACHED_ERRORS_LIMIT = "03001503";

        //test questions
        public static string INVALID_TEST_QUESTION = "03001601";
        public static string TEST_IS_FINISHED = "03001602";
        public static string CANT_CHANGE_ANSWER = "03001603";
        public static string CANT_RETURN_TO_QUESTION = "03001604";
        public static string QUESTIONS_ARE_GENERATED_FOR_THIS_TEST = "03001605";
        public static string NEED_ADMIN_CONFIRMATION = "03001606";
        public static string WAIT_THE_START_TIME = "03001607";
        public static string INVALID_ANSWER_STATUS = "03001608";
        public static string INVALID_EVALUATOR_FOR_THIS_TEST = "03001609";
        public static string CANT_VIEW_TEST_RESULT = "03001610";

        //test type
        public static string INVALID_STATUS = "03001701";
        public static string INVALID_TEST_TYPE = "03001702";
        public static string INSUFFICIENT_QUESTIONS_IN_CATEGORY = "03001703";
        public static string INSUFFICIENT_QUESTIONS_IN_TEST_TYPE = "03001704";
        public static string INVALID_QUESTION_COUNT = "03001705";
        public static string ONLY_PENDING_TEST_CAN_BE_CHANGED = "03001706";
        public static string EMPTY_RULES = "03001707";
        public static string ONLY_ACTIVE_TEST_CAN_BE_CLOSED = "03001708";
        public static string CLOSED_TEST_TYPE_CANT_BE_CHANGED = "03001709";
        public static string INVALID_DURATION = "03001710";
        public static string INVALID_MIN_PERCENT = "03001711";
        public static string INVALID_MAX_ATTEMPTS = "03001712";
        public static string INVALID_TYPE = "03001713";
        public static string INVALID_POLL_SETTINGS = "03001714";
        public static string INEXISTENT_POLL_IN_EVENT = "03001715";
        public static string POLL_ISNT_TERMINATED = "03001716";
        public static string INVALID_QUESTION_COUNT_PER_PAGE = "03001717";
        public static string INVALID_COMBINATION = "03001718";
        public static string MISMATCH_QUESTION_COUNT_AND_SELECTED = "03001719";
        public static string INVALID_MAX_ERRORS = "03001720";
        public static string ONLY_CLOSED_TEST_TYPE_CAN_BE_CLONED = "03001721";
        public static string INVALID_TIME = "03001722";
        public static string INVALID_POLL = "03001723";
        public static string ONLY_INACTIVE_TEST_CAN_BE_DELETED = "03001724";

        //test type question category 
        public static string QUESTION_COUNT_REACHED_THE_LIMIT = "03001801";
        public static string INVALID_SEQUENCE = "03001802";
        public static string INVALID_SELECTION_TYPE = "03001803";
        public static string INVALID_SEQUENCE_TYPE = "03001804";
        public static string QUESTION_COUNT_MUST_BE_EQUAL_TO_SELECTED_COUNT = "03001805";
        public static string POLLS_ACCEPTS_ONLY_ONE_ANSWER_QUESTIONS = "03001806";

        //verify tests
        public static string CANT_CALCULATE_RESULT = "03001901";
        public static string ONLY_TERMINATED_TEST_CAN_BE_SET_VERIFIED = "03001902";
        public static string NOT_ALL_ANSWERS_ARE_VERIFIED = "03001903";
        public static string INVALID_STATUS_FOR_VERIFY = "03001904";
        public static string QUESTION_POINTS_LIMIT_WRONG = "03001905";

        //user
        public static string INVALID_USER = "03002001";
        public static string INVALID_TEST_TYPE_QUESTION_CATEGORY = "03002002";
    }
}
