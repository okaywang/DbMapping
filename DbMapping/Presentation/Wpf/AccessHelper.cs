using DbMapping.Entities;
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

        public static MappingEntity[] GetRules()
        {
            var sql = "select * from Mapping";
            var entities = new List<MappingEntity>();
            using (var cnn = new OleDbConnection(AppConsts.AppConnectionString))
            {
                cnn.Open();
                using (var cmm = new OleDbCommand(sql, cnn))
                {
                    var reader = cmm.ExecuteReader();
                    while (reader.Read())
                    {
                        var entity = new MappingEntity();
                        entity.ID = (int)reader["ID"];
                        entity.MappingName = reader["MappingName"].ToString();
                        entity.ImportingMaxCount = (int)reader["ImportingMaxCount"];
                        entity.SourceIndendityFieldName = reader["SourceIndendityFieldName"].ToString();
                        entity.SourceFileName = reader["SourceFileName"].ToString();
                        entity.SourceTableName = reader["SourceTableName"].ToString();
                        entity.TargetTableType = (TargetModel.TargetTableType)reader["TargetTableType"];
                        entity.SourceFields = reader["SourceFields"].ToString();
                        entity.TargetFields = reader["TargetFields"].ToString();
                        entity.ImportedMaxIndendity = (int)reader["ImportedMaxIndendity"];
                        entities.Add(entity);
                    }
                }
            }
            return entities.ToArray();
        }
        public static MappingEntity GetRule(int id)
        {
            var sql = string.Format("select * from Mapping where ID={0}", id);
            return GetRuleBySql(sql);
        }
        public static MappingEntity GetRule(TargetModel.TargetTableType table)
        {
            var sql = string.Format("select * from Mapping where TargetTableType={0}", (int)table);
            return GetRuleBySql(sql);
        }

        private static MappingEntity GetRuleBySql(string sql)
        {
            using (var cnn = new OleDbConnection(AppConsts.AppConnectionString))
            {
                cnn.Open();
                using (var cmm = new OleDbCommand(sql, cnn))
                {
                    var reader = cmm.ExecuteReader();
                    while (reader.Read())
                    {
                        var entity = new MappingEntity();
                        entity.ID = (int)reader["ID"];
                        entity.MappingName = reader["MappingName"].ToString();
                        entity.ImportingMaxCount = (int)reader["ImportingMaxCount"];
                        entity.SourceIndendityFieldName = reader["SourceIndendityFieldName"].ToString();
                        entity.SourceFileName = reader["SourceFileName"].ToString();
                        entity.SourceTableName = reader["SourceTableName"].ToString();
                        entity.TargetTableType = (TargetModel.TargetTableType)reader["TargetTableType"];
                        entity.SourceFields = reader["SourceFields"].ToString();
                        entity.TargetFields = reader["TargetFields"].ToString();
                        entity.ImportedMaxIndendity = (int)reader["ImportedMaxIndendity"];
                        return entity;
                    }
                    return null;
                }
            }
        }

        public static DataTable GetDataTable(string sql, string fileName)
        {
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", fileName);

            var entities = new List<MappingEntity>();
            using (var cnn = new OleDbConnection(connectionString))
            {
                var adapter = new OleDbDataAdapter(sql, cnn);
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
        }

        public static void ExecuteSql(string sql, string fileName)
        {
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", fileName);

            var entities = new List<MappingEntity>();
            using (var cnn = new OleDbConnection(connectionString))
            {
                cnn.Open();
                using (var cmm = new OleDbCommand(sql, cnn))
                {
                    cmm.ExecuteNonQuery();
                }
            }
        }
    }

    public class TableSchema
    {
        public string Name { get; set; }
    }
}
