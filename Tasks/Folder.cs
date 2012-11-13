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
    using System;
    using Sitecore.Data.Items;

    public class Folder
    {
        /// <summary>
        /// Gets the template to use for the item.
        /// </summary>
        public TemplateItem Template { get; private set; }

        /// <summary>
        /// Gets the format string to apply on the date to determine the name of the item.
        /// </summary>
        public string FormatString { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Folder"/> class.
        /// </summary>
        /// <param name="templateItem">The template item.</param>
        /// <param name="format">The format.</param>
        public Folder(TemplateItem templateItem, string format)
        {
            Sitecore.Diagnostics.Assert.IsNotNull(templateItem, "templateItem");
            Sitecore.Diagnostics.Assert.IsNotNullOrEmpty(format, "format");

            Template = templateItem;
            FormatString = format;
        }

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public string GetName(DateTime date)
        {
            return date.ToString(FormatString);
        }
    }
}
