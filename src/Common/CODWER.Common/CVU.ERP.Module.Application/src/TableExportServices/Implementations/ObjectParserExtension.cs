using System;
using System.Collections.Generic;
using System.Reflection;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities.Enums;

namespace CVU.ERP.Module.Application.TableExportServices.Implementations
{
    public static class PrinterExtensions
    {
        public static PropertyInfo GetPropertyInfo(this Type type, string propertyName)
        {
            return type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
        }

        public static string ParseByDataType(this PropertyInfo propInfo, object item)
        {
            var result = propInfo.GetValue(item, null);

            switch (result)
            {
                case DateTime:
                    result = Convert.ToDateTime(result).ToString("dd/MM/yyyy, HH:mm");
                    break;
                case List<string>:
                    result = result.ParseDataByListOfStrings();
                    break;
                case bool:
                    result = Convert.ToBoolean(result) ? "+" : "-";
                    break;
                case TestResultStatusEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case TestStatusEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case QuestionTypeEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case QuestionUnitStatusEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case TestTemplateModeEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case MedicalColumnEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case TestTemplateStatusEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case QualifyingTypeEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case UserStatusEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case AccessModeEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case SolicitedPositionStatusEnum @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case TestingLocationType @enum:
                    result = EnumMessages.Translate(@enum);
                    break;
                case null:
                    result = "-";
                    break;
            }

            return result.ToString();
        }

        private static object ParseDataByListOfStrings(this object result)
        {
            result = string.Join(", ", (List<string>)result);

            return string.IsNullOrEmpty((string)result) ? "-" : result;
        }
    }
}
