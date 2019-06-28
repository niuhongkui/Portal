using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Page<T>
    {
        public Page()
        {
            rows = new List<T>();
        }
        public Page(BaseParm parm)
        {
            PageIndex = parm.index;
            PageSize = parm.rows;
            rows = new List<T>();
        }
        public int PageIndex { set; get; }

        public int PageSize { set; get; }

        public List<T> rows { set; get; }

        public int total { set; get; }
    }
}
