using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FitLevel_RPG
{
    internal class VersionInfo
    {
        
        public static string getVersionInfo()
        {
            
            // Dev build check
        
        // Version Info
        Assembly assembly = Assembly.GetExecutingAssembly();
        FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        string version = fileVersionInfo.ProductVersion;

        StringBuilder sb = new StringBuilder();
        sb.Append("Version: ");
            sb.AppendLine(version);
         

            return sb.ToString();
        }
    }
}
