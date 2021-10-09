using Hocgido2T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hocgido2T.Controllers
{
    public class UserController : Controller
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
        [HttpGet]
        public ActionResult Information(string MaND)
        {
            NguoiDung nd = db.NguoiDungs.First(e => e.MaND.Equals(MaND));
            return View(nd);
        }


    }
}