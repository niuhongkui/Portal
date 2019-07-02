﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;

namespace DAL
{
    public class ProductDAL
    {
        private DB _db = new DB();
        public Page<product> List(BaseParm parm)
        {
            parm.Name = "%" + parm.Name + "%";
            var page = new Page<product>(parm);
            page.rows = product.Fetch(" where Name like @Name and stationId=@Id", parm)
                    .Take(parm.rows)
                    .Skip(parm.index * parm.rows)
                    .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from product where Name like @Name and stationId=@Id", parm);

            return page;
        }

        public ApiMessage<string> Delete(string id)
        {
            var rows = product.Delete(" where id=@0", id);
            var msg = new ApiMessage<string>();
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

        public ApiMessage<string> Edit(product t)
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
            return new ApiMessage<string>();
        }
        public ApiMessage<product> Get(string id)
        {
            var json = new ApiMessage<product>();
            var m = product.FirstOrDefault(" where id=@0", id);
            if (m == null)
            {
                json.Success = false;
                json.Msg = "不存在";
                return json;
            }
            json.Data = m;
            return json;
        }

        public Page<productunit> GetUnit()
        {
            var page = new Page<productunit>();
            var list= productunit.Fetch("");
            page.rows = list;
            page.total = list.Count;
            return page;
        }
    }
}
