using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;

namespace Incident_Library
{ //Rasmus
    internal class Database
    {
        public static string ConnectionString
        {
            get
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string dbPath = Path.Combine(baseDir, "IncidentLibrary.db");
                return $"Data Source={dbPath};";
            }
        }
    }
}
