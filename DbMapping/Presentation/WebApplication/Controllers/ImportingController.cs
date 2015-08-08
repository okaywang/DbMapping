using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

namespace WebApplication.Controllers
{
    public class ImportingController : ApiController
    {
        public string GongFen(TargetModel.GongFenModel[] models)
        {
            var props = typeof(TargetModel.GongFenModel).GetProperties();
            var sb = new StringBuilder();
            foreach (var item in props)
            {
                sb.Append(item.Name);
                sb.Append(",");
            }

            var sql = new StringBuilder();
            sql.AppendFormat("insert into gongfen({0})", sb.ToString().TrimEnd(','));
            sql.Append("values");


            foreach (var item in models)
            {
                sb.Clear();
                sb.Append("(");
                var values = new StringBuilder();
                foreach (var prop in props)
                {
                    values.AppendFormat("'{0}'", prop.GetValue(item));
                    values.Append(",");
                }
                sb.Append(values.ToString().TrimEnd(','));
                sb.Append(")");

                sql.Append(sb.ToString());
                sql.Append(",");
            }

            var mysql = sql.ToString().TrimEnd(',');

            return mysql;
        }
    }
}
