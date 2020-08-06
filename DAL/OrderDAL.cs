﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Model;
using Model.Entity;
using Newtonsoft.Json;

namespace DAL
{
    public class OrderDAL
    {
        private DB _db = new DB();

        public List<CartEx> GetPrice(List<PriceEx> p)
        {
            var list = _db.Query<CartEx>(
                @"SELECT p1.ProductID,p1.Price,p1.MemberPrice MPrice,p1.UnitName,p2.ID UnitID,p.Name ProductName,0 Amount,p3.Url  
                  FROM product p
                  LEFT JOIN productprice  p1 ON p.ID=p1.ProductID
                  LEFT JOIN (SELECT * FROM productimg n WHERE n.RowNO=0) p3 ON p3.ProductID=p.ID
                  LEFT JOIN productunit p2 ON p1.UnitID = p2.ID
                      WHERE p.ID in (@0) AND p2.ID in (@1) AND p.IsActive=1", p.Select(n => n.ProductID), p.Select(n => n.UnitID)).ToList();
            return list;
        }

        public ApiMessage<object> Save(OrderData model)
        {
            var api = new ApiMessage<object>();
            api.Success = false;
            using (var tran = new PetaPoco.Transaction(_db))
            {
                order om = model;
                om.Insert();

                foreach (var od in model.Detail)
                {
                    //验证价格
                    var priceModel = productprice.FirstOrDefault("where ProductID=@0 and  UnitID=@1", od.ProductID, od.UnitID);
                    if (model.IsMember == 0)
                    {
                        if (priceModel.Price != od.Price)
                        {
                            api.Success = false;
                            api.Msg = "价格有误，请重新下单";
                            tran.Dispose();
                            return api;
                        }
                    }
                    else
                    {
                        if (priceModel.MemberPrice != od.Price)
                        {
                            api.Success = false;
                            api.Msg = "价格有误，请重新下单";
                            tran.Dispose();
                            return api;
                        }
                    }
                    //订单从表
                    var node = JsonConvert.DeserializeObject<orderdetail>(JsonConvert.SerializeObject(od));
                    node.Insert();
                    //清购物车
                    cart.Delete("where productid=@0 and UserID=@1", od.ProductID, model.UserID);
                    //锁库存
                    var stoModel = store.FirstOrDefault("where ProductID=@0 and  UnitID=@1", od.ProductID, od.UnitID);
                    if (stoModel.Amount < node.Amount)
                    {
                        api.Success = false;
                        api.Msg = "库存不足，请重新下单";
                        tran.Dispose();
                        return api;
                    }
                    stoModel.Amount = stoModel.Amount - node.Amount;
                    stoModel.LuckAmount = stoModel.LuckAmount + node.Amount;
                    stoModel.UpdateDate = DateTime.Now;
                    stoModel.Update();

                }

                tran.Complete();
                api.Success = true;
                api.Data = new { om.OrderNo, om.Money };
            }
            return api;
        }


        public Page<OrderData> List(BaseParm parm)
        {
            var page = new Page<OrderData>(parm);
            var strSql = new StringBuilder();
            strSql.Append("select * from  `order` where 1=1");
            strSql.Append(" And UserID=@Id");
            if (parm.Type != "全部")
            {
                strSql.Append(" AND State = @Type");
            }
            strSql.Append(" ORDER BY CreateDate DESC");
            var list = _db.SkipTake<OrderData>(parm.page * parm.rows, parm.rows, strSql.ToString(), parm);
            if (list.Any())
            {
                var details = _db.Query<OrderDetail>(@"select o.*,p1.Url from orderdetail o 
                                LEFT JOIN product p ON p.ID=o.ProductID
                              LEFT JOIN (SELECT * FROM productimg n WHERE n.RowNO=0) p1 ON p1.ProductID=p.ID
                                where o.orderno in (@0)", list.Select(n => n.OrderNo)).ToList();
                list.ForEach(n =>
                {
                    n.Detail = details.Where(m => m.OrderNo == n.OrderNo).ToList();
                });
            }

            page.rows = list;
            return page;
        }

        public Page<order> ListByPage(BaseParm parm)
        {
            var page = new Page<order>(parm);
            var strSql = new StringBuilder();
            strSql.Append(" where 1=1");
            if (!string.IsNullOrEmpty(parm.Name))
            {
                parm.Name = "%" + parm.Name + "%";
                strSql.Append(" AND UserName like @Name");
            }
            if (!string.IsNullOrEmpty(parm.Type))
            {
                strSql.Append(" AND State = @Type");
            }
            if (!string.IsNullOrEmpty(parm.Code))
            {
                strSql.Append(" AND OrderNo = @Code");
            }

            var list = _db.Page<order>(parm.page, parm.rows, strSql.ToString(), parm);

            page.rows = list.Items;
            page.total = (int)list.TotalItems;
            return page;
        }

        public Page<orderdetail> GetOrder(string orderNo)
        {
            var list = orderdetail.Fetch(@"where orderNo=@0", orderNo).ToList();
            var api = new Page<orderdetail>();
            api.rows = list;
            return api;
        }

        public ApiMessage<string> PickUp(string orderNo)
        {
            var api = new ApiMessage<string>();
            var model = order.Query(" where orderNo=@0", orderNo).FirstOrDefault();
            if (model?.State == "待取货")
            {
                model.State = "已关闭";

                model.Update();
                api.Success = true;
                api.Msg = "取货成功";
                return api;
            }
            else
            {
                api.Msg = "非待取货,不能取货";
                api.Data = "非待取货,不能取货";
                api.Success = false;
                return api;
            }
        }

        public ApiMessage<bool> DelOrder(string orderNo)
        {
            var api = new ApiMessage<bool>();
            api.Msg = "数据有误";
            api.Success = false;
            using (var tran = new PetaPoco.Transaction(_db))
            {
                var model= order.FirstOrDefault("where orderNo =@0", orderNo);
                if (model.State != "已关闭")
                    return api;
                orderdetail.Delete("where orderNo =@0", orderNo);
                order.Delete("where orderNo =@0", orderNo);
                tran.Complete();
            }
            api.Msg = "删除成功";
            api.Success = true;
            return api;
        }
    }
}
