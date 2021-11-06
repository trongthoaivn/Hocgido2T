using Hocgido2T.Class;
using Hocgido2T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hocgido2T.Controllers
{
    public class AdminManageController : Controller
    {
        public dbhocgido db = new dbhocgido();
        Crypto cryto = new Crypto();
        // GET: AdminManage
        public ActionResult Index()
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userID");
                String maTK = cryto.Decrypt(cookie.Value);
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(maTK) && ( p.TaiKhoan.Quyen.Equals("113")||p.TaiKhoan.Quyen.Equals("114")));

                if (nd != null)
                {
                    return View(nd);
                }
                else
                    return RedirectToAction("Login", "AdminManage");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "AdminManage");
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ManageUsers()
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userID");
                String maTK = cryto.Decrypt(cookie.Value);
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(maTK) && (p.TaiKhoan.Quyen.Equals("113") || p.TaiKhoan.Quyen.Equals("114")));

                if (nd != null)
                {
                    return View(nd);
                }
                else
                    return RedirectToAction("Login", "AdminManage");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "AdminManage");
            }
        }
        public ActionResult MyProfile()
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userID");
                String maTK = cryto.Decrypt(cookie.Value);
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(maTK) && (p.TaiKhoan.Quyen.Equals("113") || p.TaiKhoan.Quyen.Equals("114")));

                if (nd != null)
                {
                    return View(nd);
                }
                else
                    return RedirectToAction("Login", "AdminManage");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "AdminManage");
            }
        }
        public ActionResult ManageCourse()
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get("userID");
                String maTK = cryto.Decrypt(cookie.Value);
                NguoiDung nd = db.NguoiDungs.FirstOrDefault(p => p.MaTK.Equals(maTK) && (p.TaiKhoan.Quyen.Equals("113") || p.TaiKhoan.Quyen.Equals("114")));

                if (nd != null)
                {
                    return View(nd);
                }
                else
                    return RedirectToAction("Login", "AdminManage");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login", "AdminManage");
            }
        }
    }
}