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

        public address GetDefault(string userid)
        {
            return address.FirstOrDefault("where userid =@0 order by isdefault,CreateDate desc ", userid);
        }

    }
}
