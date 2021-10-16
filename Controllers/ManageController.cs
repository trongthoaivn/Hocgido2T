using Hocgido2T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hocgido2T.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public dbhocgido db = new dbhocgido();
        public ActionResult Index()
        {
            Console.WriteLine(Session["userID"].ToString());
            try
            {

                if (Session["userID"].ToString() != null)
                {
                    NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(Session["userID"].ToString()));
                    return View(nd);
                }
                else
                    return RedirectToAction("Login", "Users");
            }
            catch(Exception e)
            {
                return RedirectToAction("Login", "Users");
            }
           
        }


    }
}