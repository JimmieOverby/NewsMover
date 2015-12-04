//------------------------------------------------------------------------------------------------- 
// <copyright file="NewsEventHandler.cs" company="Sitecore Shared Source">
// Copyright (c) Sitecore.  All rights reserved.
// </copyright>
// <summary>Defines the NewsEventHandler type.</summary>
// <license>
// http://sdn.sitecore.net/Resources/Shared%20Source/Shared%20Source%20License.aspx
// </license>
// <url>http://trac.sitecore.net/NewsMover/</url>
//-------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using Sitecore.Data.Items;

namespace Sitecore.Sharedsource.NewsMover
{
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Determines whether [is null or empty] [the specified s].
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified s]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
    }

    internal static class XmlExtensions
    {
        /// <summary>
        /// Gets the attribute from the XmlNode. If no attribute exists, return the default value. If the attribute is empty string then use default.
        /// </summary>
        /// <param name="configNode">The config node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <param name="default">The default value.</param>
        /// <returns></returns>
        public static string GetAttributeWithDefault(this XmlNode configNode, string attributeName, string @default)
        {
            return configNode == null || configNode.Attributes == null
                ? string.Empty
                : (configNode.Attributes[attributeName] == null
                    ? @default
                    : configNode.Attributes[attributeName].Value.IsNullOrEmpty()
                        ? @default
                        : configNode.Attributes[attributeName].Value);
        }
    }

    internal static class ItemExtensions
    {
        /// <summary>
        /// Determines whether the item is the __standard values item of the template.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if the item is the __standard values item of the template; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsStandardValues(this Item item)
        {
            if (item == null)
                return false;

            bool isStandardValue = false;

            if (item.Template.StandardValues != null)
                isStandardValue = (item.Template.StandardValues.ID == item.ID);

            return isStandardValue;
        }
    }

    internal static class EnumExtensions
    {
        /// <summary>
        /// Gets the value of the description attribute from the enum
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string ToDescription(this Enum en)
        {
            var type = en.GetType();
            var memInfo = type.GetMember(en.ToString());
            if (memInfo.Length <= 0) return en.ToString();
            var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : en.ToString();
        }

        /// <summary>
        /// 3.5 extension method for .net 4's Enum.TryParse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theEnum">The enum.</param>
        /// <param name="value">The value.</param>
        /// <param name="ignoreCase">StringComparison.OrdinalIgnoreCase or StringComparison.Ordinal</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool TryParse<T>(this T theEnum, string value, bool ignoreCase, out T result)
        {
            result = theEnum;
            if (Enum.GetNames(typeof(T)).Any(item => string.Equals(item, value, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)))
            {
                result = (T)Enum.Parse(typeof(T), value);
                return true;
            }

            var type = System.Convert.ChangeType(value, (Enum.GetUnderlyingType(typeof (T))));
            if (type == null || !Enum.IsDefined(typeof (T), type)) return false;
            result = (T)Enum.Parse(typeof(T), value);
            return true;
        }
    }
}
