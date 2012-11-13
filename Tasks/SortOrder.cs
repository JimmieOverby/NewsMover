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
    using System.ComponentModel;

    public enum SortOrder
    {
        [Description("{781247D2-9785-400F-8935-C818EC757967}")] // default
        Ascending,
        [Description("{C3E3F0E3-0162-4F1F-AB3E-40348E371A3F}")] // reverse
        Descending,
        None
    }
}
