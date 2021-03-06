﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using TargetModel;
using System.Data.OracleClient;

namespace WebApplication.Controllers
{
    public class ImportingController : ApiController
    {
        [HttpGet]
        public string TestGet()
        {
            var ss = Environment.Is64BitProcess;

            string cnstr = @"Data Source=mas;User Id=foaapp;Password=foaapp;";

            using (var cn = new OracleConnection(cnstr))
            {
                cn.Open();
            }
            return "hello,Get," + DateTime.Now.ToString();
        }

        [HttpGet]
        public HttpResultModel TestObject()
        {
            var result = new HttpResultModel();
            result.Status = 0;
            result.Message = "success";

            return result;
        }

        [HttpPost]
        public string TestPost()
        {
            return "hello,Post," + DateTime.Now.ToString();
        }

        [HttpPost]
        public HttpResultModel GongFen(TargetModel.GongFenModel[] models)
        {
            var props = typeof(TargetModel.GongFenModel).GetProperties();
            var sb = new StringBuilder();
            foreach (var item in props)
            {
                sb.Append(item.Name);
                sb.Append(",");
            }

            var sql = new StringBuilder();
            sql.AppendFormat("insert into t_gfb({0})", sb.ToString().TrimEnd(','));



            foreach (var item in models)
            {
                sb.Clear();
                sql.Append("select ");
                var values = new StringBuilder();
                foreach (var prop in props)
                {
                    var v = prop.GetValue(item);
                    if (v == null)
                    {
                        values.AppendFormat("NULL");
                    }
                    else
                    {
                        values.AppendFormat("'{0}'", v);
                    }

                    values.Append(",");
                }
                sb.Append(values.ToString().TrimEnd(','));
                sb.Append(" from dual");

                sql.AppendLine(sb.ToString());
                sql.AppendLine("union");
            }

            var mysql = sql.Remove(sql.Length - 5, 5);

            string cnstr = @"Data Source=mas;User Id=foaapp;Password=foaapp;";
            try
            {
                using (var cn = new OracleConnection(cnstr))
                {
                    cn.Open();
                    using (var cm = new OracleCommand(mysql.ToString(), cn))
                    {
                        cm.ExecuteNonQuery();
                        return new HttpResultModel() { Status = 0, Message = "success" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new HttpResultModel { Status = 1, Message = ex.Message };
            }

        }
    }
}
