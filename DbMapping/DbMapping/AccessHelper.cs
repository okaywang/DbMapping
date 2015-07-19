using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMapping
{
    public class AccessHelper
    {
        public static TableSchema[] GetTables(string accessFileName)
        {
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", accessFileName);
            var connection = new OleDbConnection(connectionString);
            connection.Open();
            string[] restrictions = new string[4];
            restrictions[3] = "Table";
            var userTables = connection.GetSchema("Tables", restrictions);

            var tables = new List<TableSchema>();
            foreach (DataRow item in userTables.Rows)
            {
                tables.Add(new TableSchema { Name = item["Table_Name"].ToString() });
            }
            return tables.ToArray();
        }

        public static T[] GetList<T>(string sql, string accessFileName) where T : class
        {
            

            return null;
        }
    }

    public class TableSchema
    {
        public string Name { get; set; }
    }
}
