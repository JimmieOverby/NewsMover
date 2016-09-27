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

namespace Sitecore.Sharedsource.Tasks
{
    using System.Xml;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Sharedsource.NewsMover;

    internal class TemplateConfigurationBuilder
    {
        /// <summary>
        /// Creates a template configuration from the XmlNode
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="configNode">The config node.</param>
        /// <returns></returns>
        /// <summary>
        public static TemplateConfiguration Create(Database database, XmlNode configNode)
        {
            Sitecore.Diagnostics.Assert.IsNotNull(database, "Database");
            Sitecore.Diagnostics.Assert.IsNotNull(configNode, "XmlNode");

            string template = configNode.Attributes["id"].Value;
            string yearTemplate = null;
            string folderTemplate = null;
            string yearFormat = null;
            string monthTemplate = null, monthFormat = null;
            string dayTemplate = null, dayFormat = null;
            string dateField = null;

            Sitecore.Diagnostics.Assert.IsNotNullOrEmpty(template, "Template");
            //Sitecore.Diagnostics.Assert.IsNotNullOrEmpty(yearTemplate, "YearTemplate");
            //Sitecore.Diagnostics.Assert.IsNotNullOrEmpty(dateField, "DateField");

            // make sure we have the template of the items we want to move
            TemplateItem templateItem = database.Templates[template];

            if (configNode["DateField"] != null)
            {
                dateField = configNode["DateField"].InnerText;
            }
            if (templateItem == null)
            {
                Sitecore.Diagnostics.Log.Warn(string.Format("Template '{0}' not found.", template), configNode);
                return null;
            }

            if (configNode["YearTemplate"] != null)
            {
                monthTemplate = configNode["YearTemplate"].InnerText;
                monthFormat = configNode["YearTemplate"].GetAttributeWithDefault("formatString", "yyyy");
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
            if (configNode["FolderTemplate"] != null)
            {
                folderTemplate = configNode["FolderTemplate"].InnerText;
            }

            string sort = configNode.GetAttributeWithDefault("sort", null);
            SortOrder s = SortOrder.None;
            if (!string.IsNullOrEmpty(sort))
            {
                EnumExtensions.TryParse(s, sort, true, out s);
            }

            return new TemplateConfiguration(database, template, dateField, yearTemplate, monthTemplate, dayTemplate, folderTemplate, s, yearFormat, monthFormat, dayFormat);
        }
    }
}
