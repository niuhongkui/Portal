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
        public ApiMessage<bool> CartAdd(cart parm)
        {
            return cartDAL.Add(parm);
        }

        public ApiMessage<List<CartEx>> CartList(string userId)
        {
            return cartDAL.List(userId);
        }

        public ApiMessage<bool> CartDel(string id,int t=0)
        {
            var api=new ApiMessage<bool>();
            api = t == 0 ? cartDAL.Delete(new List<string> {id}) : cartDAL.DelAll(id);
            return api;
        }
    }
}
