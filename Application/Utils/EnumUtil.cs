using System;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public static class EnumUtil
    {
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}