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
    public class FavoriteDAL
    {
        private DB _db = new DB();

        public ApiMessage<bool> Exists(favorite parm)
        {
            var msg = _db.Query< favorite > (@"SELECT * FROM FAVORITE WHERE UserID=@UserID AND ProductID=@ProductID", parm);
            var api = new ApiMessage<bool>() { Data = msg.Any()  };
            return api;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="msgType">1 add 0 del</param>
        /// <returns></returns>
        public ApiMessage<bool> AddOrDel(favorite parm, int msgType)
        {
            var api = new ApiMessage<bool>();
            if (msgType == 1)
            {
                var list = _db.Query<favorite>(@"SELECT * FROM FAVORITE WHERE UserID=@UserID AND ProductID=@ProductID", parm);
                if (list.Any())
                {
                    var node = list.FirstOrDefault();
                    node.Amount = parm.Amount + node.Amount;
                    node.CreatDate = DateTime.Now;
                    node.Update();
                }
                else
                {
                    parm.CreatDate = DateTime.Now;
                    parm.ID = Guid.NewGuid().ToString();
                    parm.Insert();
                }
                api.Data = true;
                api.Msg = "收藏成功";
                return api;
            }
            else
            {
                var rows = _db.Execute(@"DELETE FROM FAVORITE WHERE UserID=@UserID AND ProductID=@ProductID",parm);
                if (rows > 0)
                {
                    api.Msg = "移除成功";
                }
                else
                {
                    api.Msg = "移除失败";
                    api.Success = false;
                }
                api.Data = false;
                return api;
            }
        }

        public Page<FavoriteEx> List(BaseParm parm)
        {
            var page = new Page<FavoriteEx>(parm);
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT f.*,p.Price,p.MemberPrice MPrice 
                    FROM FAVORITE f 
                    LEFT JOIN price p ON p.ProductID=f.ProductID AND p.UnitName=f.UserName");
            strSql.Append(" Where f.UserID=@Id");
            page.rows = _db.Query<FavoriteEx>(strSql.ToString(), parm)
                .Take(parm.rows)
                .Skip(parm.index * parm.rows)
                .ToList();
            return page;
        }


    }
}
