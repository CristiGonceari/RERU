using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Permissions
{
    public class PermissionCodes
    {
        //Question Category
        public const string View_Question_Category_Details = "P03010101";
        public const string View_Question_Categories = "P03010102";
        public const string Add_Question_Category = "P03010103";
        public const string Edit_Question_Category = "P03010104";
        public const string Delete_Question_Category = "P03010105";

        //Question Unit
        public const string View_Question_Unit_Details = "P03010201";
        public const string View_Question_Units = "P03010202";
        public const string Change_Question_Unit_Status = "P03010203";
        public const string Add_Question_Unit = "P03010204";
        public const string Edit_Question_Unit = "P03010205";
        public const string Delete_Question_Unit = "P03010206";
        public const string Download_Excel_Template = "P03010207";
        public const string Bulk_Questions_Upload = "P03010208";

        //Options
        public const string View_Option_Details = "P03010301";
        public const string View_Options = "P03010302";
        public const string Add_Option = "P03010303";
        public const string Edit_Option = "P03010304";
        public const string Delete_Option = "P03010305";

        //Test Type
        public const string View_Test_Type_Details = "P03010401";
        public const string View_Test_Types = "P03010402";
        public const string Get_Test_Type_Settings = "P03010403";
        public const string Get_Test_Type_By_Status = "P03010404";
        public const string Get_Test_Type_Rules = "P03010405";
        public const string Add_Test_Type = "P03010407";
        public const string Update_Test_Type = "P03010408";
        public const string Add_Rules_To_Test_Type = "P03010409";
        public const string Change_Test_Type_Status = "P03010410";
        public const string Change_Test_Type_Settings = "P03010411";
        public const string Delete_Test_Type = "P03010412";
        public const string Create_Test_Type = "P03010413";
        public const string Clone_Test_Type = "P03010414";

        //Test Type Question Category 
        public const string Assign_Question_Category_To_Test_Type = "P03010501";
        public const string Get_Test_Type_Question_Category = "P03010502";
        public const string Delete_Question_Category_From_Test_Type = "P03010503";
        public const string Edit_Test_Type_Question_Category = "P03010504";

        //Tests
        public const string Get_Test = "P03010601";
        public const string Get_Test_List = "P03010602";
        public const string Get_My_Tests = "P03010603";
        public const string Get_Test_Status = "P03010604";
        public const string Create_Test = "P03010605";
        public const string Update_Test = "P03010606";
        public const string Finalize_Test = "P03010607";
        public const string Allow_To_Start_Test = "P03010608";
        public const string Generate_My_Test = "P03010609";
        public const string View_My_Tests_Wiyhout_Event = "P03010611";

        //Test Questions
        public const string GetTestQuestion = "P03010701";
        public const string GenerateTestQuestions = "P03010702";
        public const string GetTestQuestionsSummary = "P03010703";
        public const string SaveTestQuestion = "P03010704";
        public const string PreviewTestQuestions = "P03010705";

        //Verify Tests
        public const string GetVerifyTestQuestion = "P03010801";
        public const string GetVerifyTestQuestionsSummary = "P03010802";
        public const string GetTestVerificationProgress = "P03010803";
        public const string SetVerifiedTestQuestion = "P03010804";
        public const string SetTestAsVerified = "P03010805";

        //Articles
        public const string CreateEditArticle = "P03010901";
        public const string DeleteArticle = "P03010902";
        public const string GetArticle = "P03010903";
        public const string GetArticlesList = "P03010904";

        //Users
        public const string View_User_Profiles = "P03011001";
        public const string CreateUserProfile = "P03011002";
        public const string BulkUsersIdentificatorsUpload = "P03011003";

        //Locations
        public const string Locations = "P03011101";
        public const string Location_Details = "P03011102";
        public const string Add_Location = "P03011103";
        public const string Edit_Location = "P03011104";
        public const string Delete_Location = "P03011105";
        public const string Attach_Location_Responsible_Person = "P03011106";
        public const string View_Location_Responsible_Person = "P03011107";
        public const string View_Location_Computers = "P03011108";
        public const string Attach_Computer_to_Location = "P03011109";
        public const string Detach_Computer_From_Location = "P03011110";
        public const string Detach_Location_Responsible_Person = "P03011111";
        public const string View_My_Locations = "P03011112";

        //Events
        public const string Events = "P03011201";
        public const string Event_Details = "P03011202";
        public const string Create_Event = "P03011203";
        public const string Edit_Event = "P03011204";
        public const string Delete_Event = "P03011205";
        public const string View_Event_Responsible_Person = "P03011206";
        public const string Attach_Event_Responsible_Person = "P03011207";
        public const string Detach_Event_Responsible_Person = "P03011208";
        public const string View_Event_Location = "P03011209";
        public const string Attach_Event_Location = "P03011210";
        public const string Detach_Event_Location = "P03011211";
        public const string View_Event_User = "P03011212";
        public const string Attach_Event_User = "P03011213";
        public const string Detach_Event_User = "P03011214";
        public const string Attach_Event_TestType = "P03011215";
        public const string Detach_Event_TestType = "P03011216";
        public const string View_Event_TestTypes = "P03011217";
        public const string View_My_Events = "P03011218";
        public const string View_Event_Evaluator = "P03011219";
        public const string Attach_Event_Evaluator = "P03011220";
        public const string Detach_Event_Evaluator = "P03011221";

        //Poll
        public const string Vote_Poll = "P03011301";

        //Global Settings
        public const string Global_Settings = "P03011401";
        public const string Edit_Global_Settings = "P03011402";

        //Plans
        public const string Plans = "P03011501";
        public const string Plan_Details = "P03011502";
        public const string Create_Plan = "P03011503";
        public const string Edit_Plan = "P03011504";
        public const string Delete_Plan = "P03011505";
        public const string View_Plan_Responsible_Person = "P03011506";
        public const string Attach_Plan_Responsible_Person = "P03011507";
        public const string Detach_Plan_Responsible_Person = "P03011508";
        public const string View_Plan_Event = "P03011509";
        public const string Attach_Plan_Event = "P03011510";
        public const string Detach_Plan_Event = "P03011511";

        //Statistics
        public const string Test_Questions_Statistics = "P03011601";
        public const string Category_Questions_Statistics = "P03011602";
    }
}
