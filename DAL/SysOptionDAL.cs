using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SysOptionDAL
    {
        private DB _db = new DB();

        public List<sysoption> GetOptByType(string t)
        {
            return sysoption.Query("where OptionType=@0", t).ToList();
        }
    }
}
