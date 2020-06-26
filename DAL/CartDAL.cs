using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model.Entity;

namespace DAL
{
    public class CartDAL
    {
        private DB _db = new DB();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="msgType">1 add 0 del</param>
        /// <returns></returns>
        public ApiMessage<bool> Add(cart parm)
        {
            var api = new ApiMessage<bool>();
            var list = _db.Query<cart>(@"SELECT * FROM Cart WHERE UserID=@UserID AND ProductID=@ProductID", parm).ToList();
            if (list.Any())
            {
                var node = list.FirstOrDefault();
                if (parm.Amount == 0)
                {
                    node.Delete();
                }
                else
                {
                    node.Amount = parm.Amount;
                    node.CreatDate = DateTime.Now;
                    node.Update();
                }

            }
            else
            {
                parm.CreatDate = DateTime.Now;
                parm.ID = Guid.NewGuid().ToString();
                parm.Insert();
            }
            api.Msg = "添加成功";
            return api;
        }

        public ApiMessage<List<CartEx>> List(string userId)
        {
            var list = _db.Query<CartEx>(
                @"SELECT c.ProductID,c.UnitID,c.ID,c.UnitName,c.ProductName,c.Amount,p1.Price,p1.MemberPrice MPrice,p2.Url ,p1.LimitNum,s.Amount StoreAmount
                  FROM cart c 
                  LEFT JOIN product p ON p.ID=c.ProductID
                  LEFT JOIN (SELECT * FROM productimg n WHERE n.RowNO=0) p2 on p2.ProductID=p.ID
                  LEFT JOIN productprice p1 ON p.ID = p1.ProductID AND c.UnitName=p1.UnitName
                  LEFT JOIN store s ON s.ProductID=p.ID AND s.UnitID=p1.UnitID
                  WHERE p.IsActive=1 AND c.UserID=@userId", new { userId }).ToList();
            var api = new ApiMessage<List<CartEx>>();
            api.Data = list;
            return api;
        }

        public ApiMessage<bool> Delete(List<string> ids)
        {
            var api = new ApiMessage<bool>();
            var rows = cart.Delete("where id in(@0)", ids);
            api.Data = rows > 0;
            api.Msg = rows > 0 ? "" : "删除失败";
            return api;
        }
        public ApiMessage<bool> DelAll(string id,List<string> ids=null)
        {
            var api = new ApiMessage<bool>();
            var rows = cart.Delete("where UserID=@0", id);
            api.Data = rows > 0;
            api.Msg = rows > 0 ? "" : "删除失败";
            return api;
        }
    }
}
