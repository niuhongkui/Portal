using Common;
using DAL;
using Model;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class FavoriteBLL
    {
        private FavoriteDAL _dal = new FavoriteDAL();

        private CartDAL cartDAL = new CartDAL();

        public ApiMessage<bool> Exists(favorite parm)
        {
            return _dal.Exists(parm);
        }
        public ApiMessage<bool> AddOrDel(favorite parm, int msgType)
        {
            return _dal.AddOrDel(parm,msgType);
        }

        public Page<FavoriteEx> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
        public ApiMessage<bool> CartAddOrDel(cart parm, int msgType)
        {
            return cartDAL.AddOrDel(parm, msgType);
        }

        public Page<CartEx> CartList(BaseParm parm)
        {
            return cartDAL.List(parm);
        }
    }
}
