using Hocgido2T.Class;
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
        Crypto cryto = new Crypto();
        public ActionResult Index()
        {
           
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userID");
                String maTK = cryto.Decrypt(cookie.Value);
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(maTK)&&p.TaiKhoan.Quyen.Equals("115"));
                
                if (nd != null)
                {
                    return View();
                }
                else
                    return RedirectToAction("Login", "Users");
            }
            catch(Exception e)
            {
                return RedirectToAction("Login", "Users");
            }
           
        }

        public ActionResult MyProfile()
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userID");
                String maTK = cryto.Decrypt(cookie.Value);
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(maTK) && p.TaiKhoan.Quyen.Equals("115"));
                if (nd != null)
                {
                    return View(nd);
                }
                else
                    return RedirectToAction("Login", "Users");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "Users");
            }
        }


    }
}