﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using Model;

namespace BLL
{
    public class ProductBLL
    {
        private readonly ProductDAL _dal = new ProductDAL();

        public Page<product> List(BaseParm parm)
        {
            return _dal.List(parm);
        }
        public ApiMessage<string> Delete(string id)
        {
            return _dal.Delete(id);
        }
        public ApiMessage<string> Edit(product parm)
        {
            return _dal.Edit(parm);
        }
        public ApiMessage<product> Get(string id)
        {
            return _dal.Get(id);
        }
    }
}