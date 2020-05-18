using System;
using System.ComponentModel;
using System.Reflection;

namespace Carbon.Helpers.Extensions
{
    public static class EnumExt
    {

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return (attribute == null) ? value.ToString() : attribute.Description;
        }


        public static string GetId(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(IdAttribute))
                        as IdAttribute;

            return (attribute == null) ? value.ToString() : attribute.Description;
        }

        public static string GetToolTip(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(ToolTipAttribute))
                        as ToolTipAttribute;

            return (attribute == null) ? value.ToString() : attribute.Description;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            Type type = typeof(T);
            if(!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach(FieldInfo field in type.GetFields())
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if(attribute != null)
                {
                    if(attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if(field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException("Not found.", nameof(description));

            // or return default(T);
        }


    }
}
