using System;
using System.ComponentModel;
using System.Reflection;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class EnumHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}