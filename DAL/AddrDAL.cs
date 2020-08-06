using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddrDAL
    {
        private DB _db = new DB();

        public address GetDefault(string userid,string id)
        {
            if (id.Trim(' ')=="def")
            {
                return address.FirstOrDefault("where userid =@0 order by isdefault,CreateDate desc ", userid);
            }
            else
            {
                return address.FirstOrDefault("where id =@0 ", id);
            }

        }

        public bool Save(address model)
        {
            if (model.IsDefault == 1)
            {
                _db.Execute("update address set isdefault=0 where userid=@0", model.UserID);
            }
            if (string.IsNullOrEmpty(model.ID))
            {
                model.ID = Guid.NewGuid().ToString();
                model.Insert();
                return true;
            }
            else
            {
                return model.Update() > 0;
            }
        }

        public bool Del(string id)
        {
            return address.Delete("where id=@0", id) > 0;
        }

        public Page<address> GetList(BaseParm parm)
        {
            var page = new Page<address>(parm);
            var strSql = new StringBuilder();
            strSql.Append("select * from address where userid=@Id");
            var list = address.Page(parm.page, parm.rows, strSql.ToString(), parm);
            page.rows = list.Items;
            page.total = (int)list.TotalItems;
            return page;
        }


    }
}
