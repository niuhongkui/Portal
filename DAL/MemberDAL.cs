using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Common;

namespace DAL
{
    public class MemberDAL
    {
        private DB _db = new DB();

        public bool Add(member m)
        {
            m.Insert();
            return true;
        }

        public bool UpdateVip(string orderno,string trade_no)
        {
            using (var scope = new PetaPoco.Transaction(_db))
            {
                var m = member.FirstOrDefault("where orderno=@0", orderno);
                var user = userinfo.FirstOrDefault("where id=@0", m?.UserID);
                if (user == null)
                    return false;
                
                if (m.ProID == "1")
                {
                    if (user.MemberDate > DateTime.Now)
                    {
                        user.MemberDate = user.MemberDate.Value.AddMonths(1);
                    }
                    else {
                        user.MemberDate = DateTime.Now.AddMonths(1);
                        user.IsMember = 1;
                    }
                }
                else if (m.ProID == "2")
                {
                    if (user.MemberDate > DateTime.Now)
                    {
                        user.MemberDate = user.MemberDate.Value.AddMonths(3);
                    }
                    else
                    {
                        user.MemberDate = DateTime.Now.AddMonths(3);
                        user.IsMember = 1;
                    }
                }
                else
                {
                    if (user.MemberDate > DateTime.Now)
                    {
                        user.MemberDate = user.MemberDate.Value.AddMonths(6);
                    }
                    else
                    {
                        user.MemberDate = DateTime.Now.AddMonths(6);
                        user.IsMember = 1;
                    }
                }
                m.State = "已付款";
                m.TradeNo = trade_no;
                m.Update();
                user.Update();
                scope.Complete();
            }
            return true;
        }
    }
}
