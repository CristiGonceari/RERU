using System;
using System.Collections.Generic;
using System.Linq;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CVU.ERP.Common.EnumConverters
{
    public static class EnumConverter<TEnum> where TEnum : Enum
    {
        public static List<SelectItem> SelectValues
        {
            get
            {
                return Enum.GetValues(typeof(TEnum)).OfType<TEnum>().ToList()
                    .Select(item => new SelectItem
                        {
                            Label = item.ToString(),
                            Value = Convert.ToInt32(item).ToString()
                        })
                    .OrderBy(i => i.Label)
                    .ToList();
            }
        }
    }
}
