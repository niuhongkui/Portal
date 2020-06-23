using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InStoreDAL
    {

        private DB _db = new DB();

        public Page<instore> List(BaseParm parm)
        {
            var page = new Page<instore>(parm);
            var strSql = new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND ProductName like @Name");
            }
            page.rows = instore.Fetch(strSql.ToString(), parm)
                    .Take(parm.rows)
                    .Skip(parm.index * parm.rows)
                    .ToList();
            page.total =
                _db.FirstOrDefault<int>("select count(1) from instore " + strSql, parm);
            return page;
        }

        public ApiMessage<string> Save(instore model)
        {
            var api = new ApiMessage<string>();
            model.UnitName = model.UnitID;
            model.ID = Guid.NewGuid().ToString();
            using (var scope = new PetaPoco.Transaction(_db))
            {
                var sModel = _db.FirstOrDefault<store>("where ProductID=@ProductID AND UnitID=@UnitID", model);
                if (sModel == null)
                {
                    sModel = new store();
                    sModel.Amount = model.Amount;
                    sModel.ProductID = model.ProductID;
                    sModel.ProductName = model.ProductName;
                    sModel.UnitID = model.UnitID;
                    sModel.UnitName = model.UnitID;
                    sModel.UpdateDate = DateTime.Now;
                    sModel.CreateDate = DateTime.Now;
                    sModel.ID = Guid.NewGuid().ToString();
                    sModel.OutAmount = 0;
                    sModel.OutAmount = 0;
                    _db.Insert(sModel);
                }
                else
                {
                    sModel.Amount = sModel.Amount + model.Amount;
                    _db.Update(sModel);
                }
                _db.Insert(model);
                scope.Complete();
            }
            return api;
        }

    }
}
