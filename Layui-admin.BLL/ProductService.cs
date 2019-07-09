using Layui_admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layui_admin.BLL
{
    public class ProductService : BaseService<Product>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ProductDal;
        }
    }
}
