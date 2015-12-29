using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Camarilla.RestApi.Models;

namespace Camarilla.RestApi.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }

        public static string GetDisplayName<TEnum>(this TEnum enumValue)
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            var myEnum = (enumValue as Enum);
            return myEnum.GetAttribute<DisplayAttribute>().Name;
        }

        public static string GetDisplayName<TEnum>(this TEnum? enumValue)
            where TEnum : struct, IComparable, IConvertible, IFormattable
        {
            var myEnum = (enumValue as Enum);
            return myEnum?.GetAttribute<DisplayAttribute>().Name;
        }
    }
}