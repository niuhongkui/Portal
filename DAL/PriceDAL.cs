using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;
using Model.Entity;

namespace DAL
{
    public class PriceDAL
    {
        private DB _db = new DB();

        public Page<PriceEx> List(BaseParm parm)
        {
            var page = new Page<PriceEx>(parm);
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT p.*,p1.ID ProductID ,p1.`Name` ,p1.TypeID,p1.TypeName ,s.Amount
                  FROM product p1  
                  LEFT JOIN  productprice p on p.ProductID =p1.ID
                  LEFT JOIN store s ON s.ProductID=p1.ID AND p.UnitID=s.UnitID ");
            strSql.Append(" WHERE p1.IsActive=1");
            if (!string.IsNullOrEmpty(parm.Id))
            {
                strSql.Append(" AND p1.ID = @Id");
            }
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND p1.Name like @Name");
            }
            if (!string.IsNullOrEmpty(parm.Type))
            {
                strSql.Append(" AND p1.TypeName like @Type");
            }
            var list = _db.Page<PriceEx>(parm.page, parm.rows, strSql.ToString(), parm);
            page.rows = list.Items;
            page.total = (int)list.TotalItems;
            return page;
        }

        public bool Save(productprice p)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                
                if (string.IsNullOrEmpty(p.ID))
                {//新增
                    p.ID = Guid.NewGuid().ToString();
                    _db.Insert(p);
                }
                else
                {//编辑
                    var old = _db.FirstOrDefault<productprice>("where id=@0", p.ID);
                    var model = new oldprice();
                    model.ID = Guid.NewGuid().ToString();
                    model.PriceID = old.ID;
                    model.LimitNum = old.LimitNum;
                    model.Price = old.Price;
                    model.MemberPrice = old.MemberPrice;
                    model.StaffName = p.StaffName;
                    model.CreatDate = DateTime.Now;
                    model.ProductName = "";
                    model.ProductID = old.ProductID;
                    _db.Update(p);
                    _db.Insert(model);
                }
                scope.Complete();
                return true;
            }
        }

        public bool Del(oldprice old)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                _db.Insert(old);
                _db.Delete<productprice>("where id=@0", old.PriceID);
                scope.Complete();
                return true;
            }
        }

        public PriceEx Get(string id, string proid)
        {
            var strSql = new StringBuilder();
            strSql.Append("SELECT p.*,p1.ID ProductID ,p1.`Name` ,p1.TypeID,p1.TypeName  FROM product p1  LEFT JOIN  productprice p on p.ProductID =p1.ID");
            if (string.IsNullOrEmpty(id))
            {
                id = proid;
                strSql.Append(" WHERE p1.ID=@0");
            }
            else
            {
                strSql.Append(" WHERE p.ID=@0");
            }
            return _db.FirstOrDefault<PriceEx>(strSql.ToString(), id);
        }
    }
}
