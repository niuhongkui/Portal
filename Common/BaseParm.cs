using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class BaseParm
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Query { set; get; }

        public string Code { set; get; }
        public int page { set; get; } = 0;

        public int rows { set; get; } = 15;
    }
}
