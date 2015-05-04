using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTIC_IT.Models;
using MvcTIC_IT.ViewModels;

namespace MvcTIC_IT.Controllers
{
    public class HomeController : BaseController
    {
       

        //
        // GET: /Home/

        public ActionResult Index()
        {
      
            //get all em portugal


            return View();
        }


        public ActionResult MySpecialOne()
        {
      
            return View();
        }
   


    }
}
