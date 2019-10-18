using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Common;
using Model;
using Model.Entity;

namespace Portal.Controllers.Api
{
    public class SwiperController : BaseApiController
    {

        private  SwiperBLL bll=new SwiperBLL();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiMessage<List<swiper>> List(string id)
        {
            return bll.ListByActive();
        }



    }
}