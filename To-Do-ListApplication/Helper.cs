using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_ListApplication
{
    public class Helper
    {
        public static string conString(string Name)
        {
            return ConfigurationManager.ConnectionStrings[Name].ConnectionString;
        }
    }
}
