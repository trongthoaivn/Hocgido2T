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
                khoaHoc.HinhAnh = KH.HinhAnh;
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
                khoaHoc.HinhAnh = KH.HinhAnh;
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
        [Route("api/chitietbaihoc")]
        public IHttpActionResult ChiTietBaiHoc(String mabh)
        {
            try
            {
                var baihoc = db.BaiHocs.First(p => p.MaBaiHoc.Equals(mabh));
                if (baihoc != null)
                {
                    return Json(new
                    {
                        msg = Hson.toJson(baihoc)
                    });
                }
                else return Json(new
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
                    List<BinhLuanSortViewModel> list = new List<BinhLuanSortViewModel>();
                    foreach(BinhLuan item in list1)
                    {
                        var sort = new BinhLuanSortViewModel();
                        sort.NoiDung = item.NoiDung;
                        sort.NguoiDung = item.NguoiDung.HoTen;
                        sort.NgayBL = item.NgayBL;
                        list.Add(sort);
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

        [HttpGet]
        [Route("api/themkiemtra")]
        public IHttpActionResult ThemKiemTra(String mabh)
        {
            try
            {
                if (db.BaiHocs.Any(p => p.MaBaiHoc.Equals(mabh)))
                {
                    KiemTra kiemtra = new KiemTra();
                    kiemtra.MaKT = "KT" + day + Min + sec;
                    kiemtra.MaBaiHoc = mabh;
                    db.KiemTras.Add(kiemtra);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok",
                        MaKT =kiemtra.MaKT
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
        [Route("api/ds_kiemtra")]
        public IHttpActionResult DanhSachKiemTra(String mabh)
        {
            try
            {
                var ds = db.KiemTras.Where(p => p.MaBaiHoc.Equals(mabh)).ToList();
                if (ds != null)
                {

                    List<KiemTraViewModel> list = new List<KiemTraViewModel>();
                    foreach(KiemTra item  in ds)
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
                    msg = "error",
                    
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
        [Route("api/themcauhoi")]

        public IHttpActionResult ThemCauHoi([FromBody] CauHoiViewModel cauHoiView)
        {
            try
            {
                if (db.KiemTras.Any(p => p.MaKT.Equals(cauHoiView.MaKT))){
                    CauHoi cauHoi = new CauHoi();
                    cauHoi.MaCauHoi = "CH" + day + Min + sec;
                    cauHoi.NoiDungCauHoi = cauHoiView.NoiDungCauHoi;
                    cauHoi.TheLoai = cauHoiView.TheLoai;
                    cauHoi.MaKT = cauHoiView.MaKT;

                    db.CauHois.Add(cauHoi);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok",
                        MaCH = cauHoi.MaCauHoi
                    });
                }
                else
                    return Json(new
                    {
                        msg = "error",
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
        [Route("api/themdapan")]
        public IHttpActionResult ThemDapAn([FromBody] DapAnViewModel dapAnView)
        {
            try
            {
                if (db.CauHois.Any(p => p.MaCauHoi.Equals(dapAnView.MaCauHoi)))
                {
                    DapAn dapAn = new DapAn();
                    dapAn.MaDapAn = "DA" + day + Min + sec;
                    dapAn.NoiDungDapAn = dapAnView.NoiDungDapAn;
                    dapAn.DapAnDung = dapAnView.DapAnDung;
                    dapAn.MaCauHoi = dapAnView.MaCauHoi;
                    db.DapAns.Add(dapAn);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok"
                    });
                } else
                    return Json(new
                    {
                        msg = "error",
                        
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
        [Route("api/ds_cauhoi")]
        public IHttpActionResult DanhSachCauHoi(String mabh)
        {
            try
            {
                var list = (from cauhoi in db.CauHois
                            join kiemtra in db.KiemTras on cauhoi.MaKT equals kiemtra.MaKT
                            where kiemtra.MaBaiHoc.Equals(mabh)
                            select cauhoi).ToList();
                if (list != null)
                {
                    List<CauHoiFullViewModel> result = new List<CauHoiFullViewModel>();
                    foreach (CauHoi item in list)
                    {
                        var dapan = db.DapAns.Where(p => p.MaCauHoi.Equals(item.MaCauHoi)).ToList();
                        List<DapAnViewModel> dapAns = new List<DapAnViewModel>();
                        if (dapan!= null)
                        {
                            foreach (DapAn it in dapan)
                            {
                                dapAns.Add(Hson.toJson(it));
                            }
                        }
                        result.Add(Hson.toJson(Hson.toJson(item), dapAns));
                    }
                    return Json(new
                    {
                        msg = result
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
        [Route("api/chinhsuacauhoi")]
        public IHttpActionResult ChinhSuaCauHoi([FromBody] CauHoiViewModel cauHoiView)
        {
            try
            {
                var cauhoi = db.CauHois.First(p => p.MaCauHoi.Equals(cauHoiView.MaCauHoi));
                if (cauhoi!= null)
                {
                    cauhoi.NoiDungCauHoi = cauHoiView.NoiDungCauHoi;
                    cauhoi.TheLoai = cauHoiView.TheLoai;
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok",
                       
                    });

                }
                else
                    return Json(new
                    {
                        msg = "error",
                       
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
        [Route("api/chinhsuadapan")]
        public IHttpActionResult ChinhSuaDapAn([FromBody] DapAnViewModel dapAnViewModel)
        {
            try
            {
                var dapan = db.DapAns.First(p => p.MaDapAn.Equals(dapAnViewModel.MaDapAn));
                if (dapan != null)
                {
                    if(dapAnViewModel.NoiDungDapAn!=null)
                    dapan.NoiDungDapAn = dapAnViewModel.NoiDungDapAn;
                    if(dapAnViewModel.DapAnDung!=null)
                    dapan.DapAnDung = dapAnViewModel.DapAnDung;
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok",
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
        [Route("api/xoadapan")]
        public IHttpActionResult XoaDapAn(String madapan)
        {
            try
            {
                var dapan = db.DapAns.First(p => p.MaDapAn.Equals(madapan));
                if (dapan!=null)
                {
                    db.DapAns.Remove(dapan);
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok",
                    });
                }else
                    return Json(new
                    {
                        msg = "error",
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
        [Route("api/xoacauhoi")]
        public IHttpActionResult XoaCauHoi(String mach)
        {
            try
            {
                var dapan = db.DapAns.Where(p => p.MaCauHoi.Equals(mach)).ToList();
                if (dapan != null)
                {
                    foreach (DapAn item in dapan)
                    {
                        item.MaCauHoi = null;
                        db.DapAns.Remove(item);
                    }
                    db.CauHois.Remove(db.CauHois.First(p => p.MaCauHoi.Equals(mach)));
                    db.SaveChanges();
                    return Json(new
                    {
                        msg = "ok",
                    });
                }else
                    return Json(new
                    {
                        msg = "error",
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
        [Route("api/xoabaihoc")]
        public IHttpActionResult XoaBaiHoc(String mabh)
        {
            try
            {
                var dapan = (from DA in db.DapAns
                             join CH in db.CauHois on DA.MaCauHoi equals CH.MaCauHoi
                             join KT in db.KiemTras on CH.MaKT equals KT.MaKT
                             join BH in db.BaiHocs on KT.MaBaiHoc equals BH.MaBaiHoc
                             where BH.MaBaiHoc.Equals(mabh)
                             select DA
                             ).ToList();
                if (dapan != null) db.DapAns.RemoveRange(dapan);

                var cauhoi = (from CH in db.CauHois
                              join KT in db.KiemTras on CH.MaKT equals KT.MaKT
                              join BH in db.BaiHocs on KT.MaBaiHoc equals BH.MaBaiHoc
                              where BH.MaBaiHoc.Equals(mabh)
                              select CH
                              ).ToList();
                if (cauhoi != null) db.CauHois.RemoveRange(cauhoi);

                var kiemtra = (from KT in db.KiemTras
                               join BH in db.BaiHocs on KT.MaBaiHoc equals BH.MaBaiHoc
                               where BH.MaBaiHoc.Equals(mabh)
                               select KT
                               ).ToList();
                if (kiemtra != null) db.KiemTras.RemoveRange(kiemtra);

                var binhluan = db.BinhLuans.Where(p => p.MaBaiHoc.Equals(mabh)).ToList();
                if (binhluan!=null) db.BinhLuans.RemoveRange(binhluan);

                var luu = db.LuuBaiHocs.Where(p => p.MaBaiHoc.Equals(mabh)).ToList();
                if(luu!=null) db.LuuBaiHocs.RemoveRange(luu);

                var baihoc = db.BaiHocs.First(p => p.MaBaiHoc.Equals(mabh));
                if(baihoc !=null) db.BaiHocs.Remove(baihoc);
                db.SaveChanges();
                return Json(new
                {
                    msg = "ok",
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
        [Route("api/xoakhoahoc")]
        public IHttpActionResult XoakhoaHoc(String makh)
        {
            try
            {
                var dapan = (from DA in db.DapAns
                             join CH in db.CauHois on DA.MaCauHoi equals CH.MaCauHoi
                             join KT in db.KiemTras on CH.MaKT equals KT.MaKT
                             join BH in db.BaiHocs on KT.MaBaiHoc equals BH.MaBaiHoc
                             join KH in db.KhoaHocs on BH.MaKH equals KH.MaKH
                             where KH.MaKH.Equals(makh)
                             select DA
                            ).ToList();
                if (dapan != null) db.DapAns.RemoveRange(dapan);

                var cauhoi = (from CH in db.CauHois
                              join KT in db.KiemTras on CH.MaKT equals KT.MaKT
                              join BH in db.BaiHocs on KT.MaBaiHoc equals BH.MaBaiHoc
                              join KH in db.KhoaHocs on BH.MaKH equals KH.MaKH
                              where KH.MaKH.Equals(makh)
                              select CH
                              ).ToList();
                if (cauhoi != null) db.CauHois.RemoveRange(cauhoi);

                var kiemtra = (from KT in db.KiemTras
                               join BH in db.BaiHocs on KT.MaBaiHoc equals BH.MaBaiHoc
                               join KH in db.KhoaHocs on BH.MaKH equals KH.MaKH
                               where KH.MaKH.Equals(makh)
                               select KT
                               ).ToList();
                if (kiemtra != null) db.KiemTras.RemoveRange(kiemtra);

                var binhluan = (from BL in db.BinhLuans
                                join BH in db.BaiHocs on BL.MaBaiHoc equals BH.MaBaiHoc
                                join KH in db.KhoaHocs on BH.MaKH equals KH.MaKH
                                where KH.MaKH.Equals(makh)
                                select BL
                                ).ToList();
                if (binhluan != null) db.BinhLuans.RemoveRange(binhluan);


                var luu = (from Luu in db.LuuBaiHocs
                                join BH in db.BaiHocs on Luu.MaBaiHoc equals BH.MaBaiHoc
                                join KH in db.KhoaHocs on BH.MaKH equals KH.MaKH
                                where KH.MaKH.Equals(makh)
                                select Luu
                                ).ToList();
                if (luu != null) db.LuuBaiHocs.RemoveRange(luu);

                var khoahocdk = db.KhoaHocDKs.Where(p => p.MaKH.Equals(makh)).ToList();
                if (khoahocdk!=null) db.KhoaHocDKs.RemoveRange(khoahocdk);

                var khoahoc = db.KhoaHocs.First(p => p.MaKH.Equals(makh));
                if(khoahoc!=null) db.KhoaHocs.Remove(khoahoc);

                db.SaveChanges();
                return Json(new
                {
                    msg = "ok",
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
