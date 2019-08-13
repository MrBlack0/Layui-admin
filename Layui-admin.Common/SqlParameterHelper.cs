using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.Common
{
    public class SqlParameterHelper
    {
        public static string GetParameters(string[] ids, ref object[] obj)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ids.Length; i++)
            {
                sb.AppendFormat("@{0},", i + 1);
                obj[i] = new SqlParameter($"@{i + 1}", ids[i]);
            }
            string res = sb.Remove(sb.Length - 1, 1).ToString();
            return res;
        }
    }
}
