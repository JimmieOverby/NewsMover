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
    using Data;
    using Data.Items;

    public class TemplateConfiguration
    {
        private readonly Database _database;
        private readonly string _template;
        private readonly string _yearTemplate;
        private readonly string _monthTemplate;
        private readonly string _dayTemplate;
        private readonly string _yearFormat = "yyyy";
        private readonly string _monthFormat = "MM";
        private readonly string _dayFormat = "dd";

        /// <summary>
        /// Gets the template.
        /// </summary>
        public TemplateItem Template { get; private set; }

        /// <summary>
        /// Gets the year folder.
        /// </summary>
        public Folder YearFolder { get; private set; }

        /// <summary>
        /// Gets the month folder.
        /// </summary>
        public Folder MonthFolder { get; private set; }

        /// <summary>
        /// Gets the day folder.
        /// </summary>
        public Folder DayFolder { get; private set; }

        /// <summary>
        /// Gets the date field.
        /// </summary>
        public string DateField { get; private set; }

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        public SortOrder SortOrder { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateConfiguration"/> class.
        /// </summary>
        /// <param name="database">The database.</param>
        /// <param name="template">The template.</param>
        /// <param name="dateField">The date field.</param>
        /// <param name="yearTemplate">The year template.</param>
        /// <param name="monthTemplate">The month template.</param>
        /// <param name="dayTemplate">The day template.</param>
        /// <param name="sort">The sort.</param>
        /// <param name="yearFormat">The year format.</param>
        /// <param name="monthFormat">The month format.</param>
        /// <param name="dayFormat">The day format.</param>
        internal TemplateConfiguration(Database database, string template, string dateField, string yearTemplate, string monthTemplate, string dayTemplate, SortOrder sort = SortOrder.None, string yearFormat = "yyyy", string monthFormat = "MM", string dayFormat = "dd")
        {
            Diagnostics.Assert.IsNotNull(database, "Database");
            Diagnostics.Assert.IsNotNullOrEmpty(template, "Template");
            Diagnostics.Assert.IsNotNullOrEmpty(dateField, "DateField");
            Diagnostics.Assert.IsNotNullOrEmpty(yearTemplate, "YearTemplate");

            _database = database;
            _template = template;
            _yearTemplate = yearTemplate;
            _monthTemplate = monthTemplate;
            _dayTemplate = dayTemplate;
            _yearFormat = yearFormat;
            _monthFormat = monthFormat;
            _dayFormat = dayFormat;
            DateField = dateField;
            SortOrder = sort;

            CreateFolders();
        }

        /// <summary>
        /// Creates the folders.
        /// </summary>
        private void CreateFolders()
        {
            // make sure we have the template of the items we want to move
            Template = _database.Templates[_template];

            // we at least need a year template
            YearFolder = new Folder(_database.Templates[_yearTemplate], _yearFormat);

            // we may want to organize in months too
            if (!string.IsNullOrEmpty(_monthTemplate))
            {
                MonthFolder = new Folder(_database.Templates[_monthTemplate], _monthFormat);
            }

            // we may also want to put in day folders
            if (!string.IsNullOrEmpty(_dayTemplate))
            {
                DayFolder = new Folder(_database.Templates[_dayTemplate], _dayFormat);
            }

            // make sure we have a Month if we have a Day
            Diagnostics.Assert.IsFalse(MonthFolder == null && DayFolder != null, "dayTemplate without monthTemplate");
        }
    }
}
