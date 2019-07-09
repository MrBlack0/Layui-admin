using Layui_admin.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.EFDAL
{
    public class DbContextFactory
    {
        public static DbContext GetDbcontext()
        {
            //return new WebSiteMrBlackEntities();
            //一次请求公用一个上下文实例
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new WebSiteTemplate1Entities();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
    }
}
