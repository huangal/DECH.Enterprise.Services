using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace DECH.Enterprise.Services.Customers.Contracts
{
    public static class ApplicationExtensions
    {

        /// <summary>
        /// Tries to parse a string into an enum honoring EnumMemberAttribute if present
        /// </summary>
        public static bool TryParseWithMemberName<TEnum>(this string value, out TEnum result) where TEnum : struct
        {
            result = default;

            if (string.IsNullOrEmpty(value))
                return false;

            Type enumType = typeof(TEnum);

            foreach (string name in Enum.GetNames(typeof(TEnum)))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    result = Enum.Parse<TEnum>(name);
                    return true;
                }

                EnumMemberAttribute memberAttribute
                    = enumType.GetField(name).GetCustomAttribute(typeof(EnumMemberAttribute)) as EnumMemberAttribute;

                if (memberAttribute is null)
                    continue;

                if (memberAttribute.Value.Equals(value, StringComparison.OrdinalIgnoreCase))
                {
                    result = Enum.Parse<TEnum>(name);
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Gets the enum value from a string honoring the EnumMemberAttribute if present 
        /// </summary>
        public static TEnum? GetEnumValueOrDefault<TEnum>(this string value) where TEnum : struct
        {
            if (TryParseWithMemberName(value, out TEnum result))
                return result;

            return default;
        }







        //public static string ParseToPlain(this string value)
        //{
        //    if (string.IsNullOrEmpty(value)) return value;
        //    return value.Replace("\"", "'")
        //        .Replace("\n", " ")
        //        .Replace("\r", " ");
        //}


        //public static void LogException(this ILogger logger, string value)
        //{
        //    if (string.IsNullOrWhiteSpace(value)) return;

        //    logger.LogError($"\"{value.ParseToPlain()}\"");
        //}

        //public static void LogException(this ILogger logger, object exception, string value = null)
        //{
        //    if (string.IsNullOrWhiteSpace(value)) value = string.Empty;
        //    if (exception == null && !string.IsNullOrWhiteSpace(value))
        //    {
        //        logger.LogError($"\"{value.ParseToPlain()}\"");
        //        return;
        //    }

        //    if (exception is Exception)
        //        logger.LogError($"\"{value.ParseToPlain()}\"| Exception=\"{exception.ToString().ParseToPlain()}\"");
        //}
    }
}
