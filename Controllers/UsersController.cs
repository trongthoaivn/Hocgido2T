using Hocgido2T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hocgido2T.Controllers
{
    public class UsersController : Controller
    {
        // GET: User
        public dbhocgido db = new dbhocgido();
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        


    }
}