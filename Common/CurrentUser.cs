using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CurrentUser
    {
        public CurrentUser()
        {
            ImageUrl = "/content/images/p1.png";
        }
        public string Id { set; get; }
        public string UserName { set; get; }
        public string UserCode { set; get; }
        public string PassWord { set; get; }
        public string ImageUrl { set; get; }
        public string UserType { set; get; }
        public string StationId { set; get; }
        public string StationName { set; get; }
        public string StationCode { set; get; }
        public string Phone { set; get; }

        public string Message { set; get; }

        public int IsMember { set; get; } = 0;

        public int PointAmount { set; get; }

        //public List<Limit> Limits { set; get; } = new List<Limit>();

    }


    public class Limit
    {
        public string Name { set; get; }
        public string ClassName { set; get; }

        public string Url { set; get; }

        public int Id { set; get; }
        public int PId { set; get; }
        public int Level { set; get; }
    }
}
