using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class Car : car
    {
        public Car()
        {
            Operator = new Operator();
        }
        public Operator Operator { set; get; }
    }
}
