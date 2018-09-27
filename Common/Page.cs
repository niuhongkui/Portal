using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Page<T>
    {
        public int PageIndex { set; get; } 

        public int PageSize { set; get; } 

        public List<T> rows { set; get; }

        public int total { set; get; }
    }
}
