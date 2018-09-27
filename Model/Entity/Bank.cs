using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Bank : bank
    {
        public Bank()
        {
            Operator = new Operator();
        }
        public Operator Operator { get; set; }
    }
}
