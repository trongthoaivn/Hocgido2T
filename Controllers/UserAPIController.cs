using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Hocgido2T.Controllers.ViewModels;
using Hocgido2T.Models;

namespace Hocgido2T.Controllers
{
    public class UserAPIController : ApiController
    {

        DateTime time = DateTime.Now;
        string day = DateTime.Now.ToString("dd");
        string month = DateTime.Now.ToString("MM");
        string year = DateTime.Now.ToString("yyyy");
        string Min = DateTime.Now.ToString("mm");
        string sec = DateTime.Now.ToString("ss");

        public dbhocgido db = new dbhocgido();

        //Danh sach user
        [HttpGet]
        [Route("api/ds_taikhoan")]
        public IHttpActionResult Danh_sach_taikhoan()
        {
            try
            {
                List<TaiKhoan> list = db.TaiKhoans.ToList();
                List<TaiKhoanViewModel> taiKhoanViews = new List<TaiKhoanViewModel>();
                foreach (TaiKhoan tk in list)
                {
                    taiKhoanViews.Add(Hson.toJson(tk));
                }
                return Json(new {
                    msg = taiKhoanViews
                });
            } catch (Exception e)
            {
                return Json(new {
                    msg = "error"
                });

            }
        }


        //chinh sua tai khoan 
        [HttpPost]
        [Route("api/chinh_sua_tai_khoan")]
        public IHttpActionResult Chinh_sua_taikhoan([FromBody] TaiKhoanViewModel taiKhoan)
        {
            try {
                var tk = db.TaiKhoans.First(e => e.MaTK.Equals(taiKhoan.MaTK));
                if (tk != null)
                {
                    tk.MatKhau = taiKhoan.MatKhau;
                    tk.Email = taiKhoan.Email;
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }
                else
                    return Json(new
                    {
                        msg = "user_not_exist"
                    });

            } catch (Exception e)
            {
                return Json(new {
                    msg = "error"
                });
            }
        }

        //Đăng ký user
        [HttpPost]
        [Route("api/dang_ky_user")]
        public IHttpActionResult Dang_ky_taikhoan([FromBody] TaiKhoanViewModel taiKhoan)
        {
            try {
                if (db.TaiKhoans.Any(e => e.Email.Equals(taiKhoan.Email)) && db.TaiKhoans.Any(e => e.TenTK.Equals(taiKhoan.TenTK)))
                {
                    return Json(new
                    {
                        msg = "user_exist"
                    });
                }
                else
                {
                    NguoiDung nd_new = new NguoiDung();
                    TaiKhoan tk_new = new TaiKhoan();
                    tk_new.MaTK = "TK" + day + Min + sec;
                    tk_new.TenTK = taiKhoan.TenTK;
                    tk_new.MatKhau = taiKhoan.MatKhau;
                    tk_new.Quyen = taiKhoan.Quyen;
                    tk_new.Email = taiKhoan.Email;
                    nd_new.MaND = "ND" + day + Min + sec;
                    nd_new.MaTK = tk_new.MaTK;

                    db.TaiKhoans.Add(tk_new);
                    db.NguoiDungs.Add(nd_new);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = nd_new.MaND
                    });
                }
            } catch (Exception e)
            {
                return Json(new {
                    msg = "error"
                });

            }

        }
        //chinh sua thong tin nguoi dung
        [HttpPost]
        [Route("api/chinh_sua_thong_tin_user")]
        public IHttpActionResult Chinh_sua_thong_tin_user([FromBody] NguoiDungViewModel nguoiDung)
        {
            try {
                var nd = db.NguoiDungs.First(e => e.MaND.Equals(nguoiDung.MaND));
                if (nd != null)
                {
                    nd.HoTen = nguoiDung.HoTen;
                    nd.NgaySinh = nguoiDung.NgaySinh;
                    nd.Sdt = nguoiDung.Sdt;
                    nd.DiaChi = nguoiDung.DiaChi;
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                } else
                    return Json(new
                    {
                        msg = "user_not_exist"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error"
                });
            }
        }

        //Danh sach nguoi dung
        [HttpGet]
        [Route("api/ds_nguoidung")]
        public IHttpActionResult Danh_sach_nguoi_dung()
        {
            try
            {
                List<NguoiDung> list = db.NguoiDungs.ToList();
                List<NguoiDungViewModel> nguoiDungViewModels = new List<NguoiDungViewModel>();
                foreach (NguoiDung tk in list)
                {
                    
                    nguoiDungViewModels.Add(Hson.toJson(tk));
                }
                return Json(new
                {
                    msg = nguoiDungViewModels
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error"
                });

            }
        }
        //Lay 1 nguoi dung
        [HttpGet]
        [Route("api/lay_thong_tin_user")]
        public IHttpActionResult Lay_thong_tin_user(string Mand)
        {
            try
            {
                var user = db.NguoiDungs.First(e => e.MaND.Equals(Mand));
                TaiKhoan tk = new TaiKhoan();
                if(user != null)
                {
                    tk = db.TaiKhoans.First(e => e.MaTK.Equals(user.MaTK));
                }
                return Json(new
                {
                    msg = "ok",
                    nguoidung = Hson.toJson(user),
                    taikhoan = Hson.toJson(tk)
                }) ;
            }catch(Exception e)
            {
                return Json(new
                {
                    msg = "error"
                });
            }
        }
        //dang nhap
        [HttpPost]
        [Route("api/dang_nhap")]
        public IHttpActionResult Dang_nhap([FromBody]TaiKhoanViewModel taiKhoan) 
        {
            try
            {
                var tk = db.TaiKhoans.First(e => e.TenTK.Equals(taiKhoan.TenTK) && e.MatKhau.Equals(taiKhoan.MatKhau));
                if(tk != null)
                {
                    return Json(new
                    {
                        msg = "ok",
                        tk = tk.MaTK
                    });
                }else
                    return Json(new
                    {
                        msg = "user_not_exist"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error"
                });
            }
        }
    }
}