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

namespace Sitecore.Sharedsource.Tasks
{
    using System.Xml;
    using Sharedsource.NewsMover;

    internal class TemplateConfigurationBuilder
    {
        /// <summary>
        /// Creates a template configuration from the XmlNode
        /// </summary>
        /// <param name="configNode">The config node.</param>
        /// <returns></returns>
        public static TemplateConfiguration Create(XmlNode configNode)
        {
            Diagnostics.Assert.IsNotNull(configNode, "XmlNode");
            var databaseName = "master";
            if (configNode["Database"] != null)
            {
                databaseName = configNode["Database"].InnerText;
            }
            var template = configNode.Attributes["id"].Value;
            var yearTemplate = configNode["YearTemplate"].InnerText;
            var yearFormat = configNode["YearTemplate"].GetAttributeWithDefault("formatString", "yyyy");
            string monthTemplate = null;
            string monthFormat = null;
            string dayTemplate = null;
            string dayFormat = null;
            var dateField = configNode["DateField"].InnerText;

            Diagnostics.Assert.IsNotNullOrEmpty(databaseName, "DatabaseName");
            Diagnostics.Assert.IsNotNullOrEmpty(template, "Template");
            Diagnostics.Assert.IsNotNullOrEmpty(yearTemplate, "YearTemplate");
            Diagnostics.Assert.IsNotNullOrEmpty(dateField, "DateField");

            var database = Configuration.Factory.GetDatabase(databaseName);

            Diagnostics.Assert.IsNotNull(database, "Database");

            // make sure we have the template of the items we want to move
            var templateItem = database.Templates[template];

            if (templateItem == null)
            {
                Diagnostics.Log.Warn(string.Format("Template '{0}' not found.", template), configNode);
                return null;
            }

            if (configNode["MonthTemplate"] != null)
            {
                monthTemplate = configNode["MonthTemplate"].InnerText;
                monthFormat = configNode["MonthTemplate"].GetAttributeWithDefault("formatString", "MM");
            }

            if (configNode["DayTemplate"] != null)
            {
                dayTemplate = configNode["DayTemplate"].InnerText;
                dayFormat = configNode["DayTemplate"].GetAttributeWithDefault("formatString", "dd");
            }


            var sort = configNode.GetAttributeWithDefault("sort", null);
            var s = SortOrder.None;
            if (!string.IsNullOrEmpty(sort))
            {
                Enum.TryParse(sort, true, out s);
            }

            return new TemplateConfiguration(database, template, dateField, yearTemplate, monthTemplate, dayTemplate, s, yearFormat, monthFormat, dayFormat);
        }
    }
}
