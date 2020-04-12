using System;
using System.ComponentModel;
using System.Reflection;

namespace IdescatApi.Serveis
{
    static public class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            return !(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                ? value.ToString() : attribute.Description;
        }

        public static T GetEnum<T>(this string enumerationDescription) where T : Enum
        {
            var type = typeof(T);

            if (!type.IsEnum)
                throw new ArgumentException("GetEnum<T>(): Must be of enum type", "T");

            foreach (T val in Enum.GetValues(type))
                if (val.GetDescription() == enumerationDescription)
                    return val;

            throw new ArgumentException("GetEnum<T>(): Invalid description for enum " + type.Name + " " + enumerationDescription);
        }
    }


}
