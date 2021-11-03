using Hocgido2T.Class;
using Hocgido2T.Controllers.ViewModels;
using Hocgido2T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hocgido2T.Controllers
{
    public class CourseAPIController : ApiController
    {
        DateTime time = DateTime.Now;
        string day = DateTime.Now.ToString("dd");
        string month = DateTime.Now.ToString("MM");
        string year = DateTime.Now.ToString("yyyy");
        string Min = DateTime.Now.ToString("mm");
        string sec = DateTime.Now.ToString("ss");

        public dbhocgido db = new dbhocgido();

        Crypto cryto = new Crypto();


        [HttpPost]
        [Route("api/themkhoahoc")]
        public IHttpActionResult ThemKhoaHoc([FromBody] KhoaHocViewModel KH)
        {
            try
            {
                KhoaHoc khoaHoc = new KhoaHoc();
                khoaHoc.MaKH = "KH" + Min + day + sec;
                khoaHoc.TenKH = KH.TenKH;
                khoaHoc.MoTaKH = KH.MoTaKH;
                khoaHoc.LuotDK = 0;
                db.KhoaHocs.Add(khoaHoc);
                db.SaveChanges();
                return Json(new {
                    msg = "ok"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpPost]
        [Route("api/suakhoahoc")]
        public IHttpActionResult ChinhsuaKhoaHoc([FromBody] KhoaHocViewModel KH)
        {
            try
            {
                KhoaHoc khoaHoc = db.KhoaHocs.First(p=>p.MaKH.Equals(KH.MaKH));
                khoaHoc.TenKH = KH.TenKH;
                khoaHoc.MoTaKH = KH.MoTaKH;
                db.SaveChanges();
                return Json(new
                {
                    msg = "ok"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpGet]
        [Route("api/ds_khoahoc")]
        public IHttpActionResult DanhSachKhoaHoc()
        {
            try
            {
                List<KhoaHoc> DS = db.KhoaHocs.ToList();
                List<KhoaHocViewModel> ds = new List<KhoaHocViewModel>();
                foreach (KhoaHoc item in DS)
                {
                    ds.Add(Hson.toJson(item));
                }
                return Json(new
                {
                    msg = ds
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }


        [HttpGet]
        [Route("api/chitietkhoahoc")]
        public IHttpActionResult ChiTietKhoaHoc(String makh)
        {
            try
            {
                var kh = db.KhoaHocs.First(p => p.MaKH.Equals(makh));
                if (kh == null)
                    return Json(new
                    {
                        msg = "error"
                    });
                return Json(new
                {
                    msg = Hson.toJson(kh)
                });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpGet]
        [Route("api/dangkykhoahoc")]
        public IHttpActionResult DangKyKhoaHoc(String userid ,String makh)
        {
            try
            {
                var KH = db.KhoaHocs.First(p => p.MaKH.Equals(makh));
                var ND = db.NguoiDungs.First(p => p.MaND.Equals(userid));
                if(KH!=null && ND != null && !db.KhoaHocDKs.Any(p=>p.MaKH.Equals(makh)&&p.MaND.Equals(userid)))
                {
                    KhoaHocDK khoaHocDK = new KhoaHocDK();
                    khoaHocDK.MaKHDK = "DK" + day + Min + sec;
                    khoaHocDK.MaND = userid;
                    khoaHocDK.MaKH = makh;
                    KH.LuotDK++;
                    db.KhoaHocDKs.Add(khoaHocDK);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }
                else
                {
                    return Json(new
                    {
                        msg = "error"
                    });
                }
                
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpGet]
        [Route("api/ds_khoahoc_user")]
        public IHttpActionResult DanhSachKhoaHocbyUser(String userid)
        {
            try
            {
                List<KhoaHocDK> list = db.KhoaHocDKs.Where(p => p.MaND.Equals(userid)).ToList();
                List<KhoaHocDKViewModel> khoaHocDKViews = new List<KhoaHocDKViewModel>();
                foreach(KhoaHocDK item in list)
                {
                    khoaHocDKViews.Add(Hson.toJson(item));
                }
                return Json(new { 
                    msg = khoaHocDKViews
                });;
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }


        [HttpGet]
        [Route("api/ds_khoahoc_kh")]
        public IHttpActionResult DanhSachKhoaHocbyKhoaHoc(String makh)
        {
            try
            {
                List<KhoaHocDK> list = db.KhoaHocDKs.Where(p => p.MaKH.Equals(makh)).ToList();
                List<KhoaHocDKViewModel> khoaHocDKViews = new List<KhoaHocDKViewModel>();
                foreach (KhoaHocDK item in list)
                {
                    khoaHocDKViews.Add(Hson.toJson(item));
                }
                return Json(new
                {
                    msg = khoaHocDKViews
                }); 
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }


        [HttpGet]
        [Route("api/huydangkykhoahoc")]
        public IHttpActionResult HuyDangkyKhoaHoc(String userid ,String makh)
        {
            try
            {
                var khdk = db.KhoaHocDKs.First(p => p.MaKH.Equals(makh) && p.MaND.Equals(userid));
                if (khdk != null)
                {
                    db.KhoaHocDKs.Remove(khdk);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }else

                    return Json(new
                    {
                        msg = "error"
                    });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }
        
        [HttpPost]
        [Route("api/thembaihoc")]
        public IHttpActionResult ThemBaiHoc([FromBody] BaiHocViewModel BH)
        {
            try
            {
                BaiHoc baiHoc = new BaiHoc();
                if (db.KhoaHocs.Any(p => p.MaKH.Equals(BH.MaKH)))
                {
                    baiHoc.MaBaiHoc = "BH" + sec + Min + day;
                    baiHoc.Video = BH.Video;
                    baiHoc.GioiThieu = BH.GioiThieu;
                    baiHoc.LyThuyet = BH.LyThuyet;
                    baiHoc.CodeMau = BH.CodeMau;
                    baiHoc.LuocHoc = "0";
                    baiHoc.MaKH = BH.MaKH;
                    db.BaiHocs.Add(baiHoc);
                    db.SaveChanges();
                }
                return Json(new
                {
                    msg = "ok"
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpGet]
        [Route("api/ds_baihoc_kh")]
        public IHttpActionResult DanhSachBaiHocbyKH(String makh)
        {
            try
            {
                var baihocs = db.BaiHocs.Where(p => p.MaKH.Equals(makh)).ToList();
                List<BaiHocViewModel> list = new List<BaiHocViewModel>();
                if (baihocs != null)
                {
                    foreach(BaiHoc item in baihocs)
                    {
                        list.Add(Hson.toJson(item));
                    }
                    return Json(new
                    {
                        msg = list
                    });
                }else
                    return Json(new
                    {
                        msg = "error"
                    });


            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpPost]
        [Route("api/chinhsuabaihoc")]
        public IHttpActionResult ChinhSuaBaiHoc([FromBody] BaiHocViewModel BH)
        {
            try
            {
                var baihoc = db.BaiHocs.First(p => p.MaBaiHoc.Equals(BH.MaBaiHoc));
                if (baihoc != null)
                {
                    baihoc.Video = BH.Video;
                    baihoc.GioiThieu = BH.GioiThieu;
                    baihoc.LyThuyet = BH.LyThuyet;
                    baihoc.CodeMau = BH.CodeMau;
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }else
                    return Json(new
                    {
                        msg = "error"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpPost]
        [Route("api/luubaihoc")]
        public IHttpActionResult LuuBaiHoc([FromBody] LuuBaiHocViewModel luuBai)
        {
            try
            {
                Boolean check = db.NguoiDungs.Any(p => p.MaND.Equals(luuBai.MaND)) && db.BaiHocs.Any(p => p.MaBaiHoc.Equals(luuBai.MaBaiHoc));
                if (check && !db.LuuBaiHocs.Any(p => p.MaBaiHoc.Equals(luuBai.MaBaiHoc) && p.MaND.Equals(luuBai.MaND))){
                    LuuBaiHoc LBH = new LuuBaiHoc();
                    LBH.MaLuu = "LB" + day + Min + sec;
                    LBH.NgayLuu = time;
                    LBH.MaND = luuBai.MaND;
                    LBH.MaBaiHoc = luuBai.MaBaiHoc;
                    db.LuuBaiHocs.Add(LBH);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }else
                    return Json(new
                    {
                        msg = "error"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpGet]
        [Route("api/ds_baihocluu_user")]
        public IHttpActionResult DanhSachBaiHocLuubyUser(String userid)
        {
            try
            {
                var list1 = db.LuuBaiHocs.Where(p => p.MaND.Equals(userid));
                List<LuuBaiHocViewModel> list2 = new List<LuuBaiHocViewModel>();
                if (list1 != null)
                {
                    foreach(LuuBaiHoc item in list1)
                    {
                        list2.Add(Hson.toJson(item));
                    }
                    return Json(new
                    {
                        msg = list2
                    });
                }
                else
                    return Json(new
                    {
                        msg = "error"
                    });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }
        
        [HttpGet]
        [Route("api/huyluubaihoc")]
        public IHttpActionResult HuyLuuBaiHoc(String userid,String mabh)
        {
            try
            {
                var lbh = db.LuuBaiHocs.First(p => p.MaND.Equals(userid) && p.MaBaiHoc.Equals(mabh));
                if (lbh != null)
                {
                    db.LuuBaiHocs.Remove(lbh);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }
                else 
                    return Json(new
                {
                    msg = "error"
                });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }


        [HttpPost]
        [Route("api/thembinhluan")]
        public IHttpActionResult ThemBinhLuan([FromBody] BinhLuanViewModel binhLuan)
        {
            try
            {
                if (db.NguoiDungs.Any(p => p.MaND.Equals(binhLuan.MaND)) && db.BaiHocs.Any(p => p.MaBaiHoc.Equals(binhLuan.MaBaiHoc)))
                {
                    BinhLuan binhLuan1 = new BinhLuan();
                    binhLuan1.MaBL = "BL" + day + Min + sec;
                    binhLuan1.NoiDung = binhLuan.NoiDung;
                    binhLuan1.MaND = binhLuan.MaND;
                    binhLuan1.MaBaiHoc = binhLuan.MaBaiHoc;
                    binhLuan1.NgayBL = time;
                    db.BinhLuans.Add(binhLuan1);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }else
                    return Json(new
                    {
                        msg = "error"
                    });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }


        [HttpGet]
        [Route("api/ds_binhluan_bh")]
        public IHttpActionResult DanhSachBinhLuan(String mabh)
        {
            try
            {
                var list1 = db.BinhLuans.Where(p => p.MaBaiHoc.Equals(mabh));
                if (list1 != null)
                {
                    List<BinhLuanViewModel> list = new List<BinhLuanViewModel>();
                    foreach(BinhLuan item in list1)
                    {
                        list.Add(Hson.toJson(item));
                    }
                    return Json(new
                    {
                        msg = list
                    });
                }else
                    return Json(new
                    {
                        msg = "error"
                    });

            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }

        [HttpGet]
        [Route("api/xoabinhluan")]
        public IHttpActionResult XoaBinhLuan(string userid, string mabl)
        {
            try
            {
                if (db.BinhLuans.Any(p => p.MaND.Equals(userid) || p.MaBL.Equals(mabl)) && db.NguoiDungs.Any(p => p.TaiKhoan.Quyen.Equals("113"))){
                    db.BinhLuans.Remove(db.BinhLuans.First(p => p.MaBL.Equals(mabl)));
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                }
                else
                    return Json(new
                    {
                        msg = "error"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    msg = "error",
                    error = e.Message
                });

            }
        }
    }
}
