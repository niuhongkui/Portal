using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace DAL
{
    public class ProductTypeDAL
    {
        private DB _db = new DB();
        public Page<producttype> List(BaseParm parm)
        { 
            var page = new Page<producttype>(parm);
            var strSql =new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND Name like @Name");
            }
            if (!string.IsNullOrEmpty(parm.Id))
            {
                strSql.Append(" AND stationId like @Id");
            }
            if (!string.IsNullOrEmpty(parm.Type))
            {
                strSql.Append(" AND TopCategoryID = @Type");
            }
            page.rows= producttype.Fetch(strSql.ToString(), parm)
                    .Take(parm.rows)
                    .Skip(parm.index*parm.rows)
                    .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from producttype "+ strSql, parm);

            return page;
        }

        public ApiMessage<string> Delete(string id)
        {
            var rows = producttype.Delete(" where id=@0", id);
            var msg=new ApiMessage<string>();
            if (rows > 0)
            {
                msg.Msg = "删除成功";
                msg.Success = true;

            }
            else
            {
                msg.Msg = "删除失败";
                msg.Success = false;

            }

            return msg;
        }

        public ApiMessage<string> Edit(producttype t)
        {
            if (string.IsNullOrEmpty(t.ID))
            {
                t.ID = Guid.NewGuid().ToString();
                t.Insert();
            }
            else
            {
                t.Update();
            }
            return new ApiMessage<string>() ;
        }
        public ApiMessage<producttype> Get(string id)
        {
            var json=new ApiMessage<producttype>();
            var m = producttype.FirstOrDefault(" where id=@0", id);
            if (m == null)
            {
                json.Success = false;
                json.Msg = "不存在";
                return json;
            }
            json.Data = m;
            return json;
        }
    }
}
