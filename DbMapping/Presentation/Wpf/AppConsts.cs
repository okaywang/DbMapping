using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMapping
{
    public class AppConsts
    {
        public static readonly string AppConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data/Mapping.accdb";

        public static readonly string ImportUrl = System.Configuration.ConfigurationManager.AppSettings["ImportingUrl"];
    }
}
