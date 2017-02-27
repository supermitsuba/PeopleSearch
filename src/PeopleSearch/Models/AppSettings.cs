using System;

namespace PeopleSearch.Settings
{
    /// <summary>
    /// Appsettings class manages the settings of the application.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// The connection was moved.  This is no longer used.
        /// </summary>
        /// <returns></returns>
        [Obsolete]
        public string ConnectionString { get; set; }
    }
}