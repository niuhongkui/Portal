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
    public class SwiperDAL
    {
        private DB _db = new DB();
        public ApiMessage<bool> Delete(string id)
        {
            var rows = swiper.Delete(" where ID=@id", id);
            return new ApiMessage<bool>() { Data = rows > 0 };
        }



        public Page<swiper> List(BaseParm parm)
        {

            var page = new Page<swiper>(parm);
            var strSql = new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND ProductName like @Name");
            }
            page.rows = swiper.Fetch(strSql.ToString(), parm)
                .Take(parm.rows)
                .Skip(parm.index * parm.rows)
                .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from Swiper " + strSql, parm);

            return page;
        }

        public ApiMessage<swiper> Get(string id)
        {
            var api = new ApiMessage<swiper>();
            api.Data = _db.Query<swiper>(@"select * from swiper where id=@id", new { id }).FirstOrDefault();

            return api;
        }
    }
}
