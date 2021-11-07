using Hocgido2T.Controllers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hocgido2T.Models;

namespace Hocgido2T.Controllers
{
    public class  Hson
    {
        public static TaiKhoanViewModel toJson(TaiKhoan obj)
        {
            TaiKhoanViewModel view = new TaiKhoanViewModel();
            view.MaTK = obj.MaTK;
            view.TenTK = obj.TenTK;
            view.MatKhau = obj.MatKhau;
            view.Email = obj.Email;
            view.Quyen = obj.Quyen;
            return view;
        }
        public static NguoiDungViewModel toJson(NguoiDung obj)
        {
            NguoiDungViewModel view = new NguoiDungViewModel();
            view.MaND = obj.MaND;
            view.HoTen = obj.HoTen;
            view.NgaySinh = obj.NgaySinh;
            view.Sdt = obj.Sdt;
            view.DiaChi = obj.DiaChi;
            view.MaTK = obj.MaTK;
            return view;
        }

        public static TinhTrangBHViewModel toJson(TinhTrangBH obj)
        {
            TinhTrangBHViewModel view = new TinhTrangBHViewModel();
            view.MaTTBH = obj.MaTTBH;
            view.MaND = obj.MaND;
            view.MaKHDDK = obj.MaKHDDK;
            return view;
        }

        public static LuuBaiHocViewModel toJson(LuuBaiHoc obj)
        {
            LuuBaiHocViewModel view = new LuuBaiHocViewModel();
            view.MaLuu = obj.MaLuu;
            view.NgayLuu = obj.NgayLuu;
            view.MaND = obj.MaND;
            view.MaBaiHoc = obj.MaBaiHoc;
            return view;
        }
        public static KiemTraViewModel toJson(KiemTra obj)
        {
            KiemTraViewModel view = new KiemTraViewModel();
            view.MaBaiHoc = obj.MaBaiHoc;
            view.MaKT = obj.MaKT;
            return view;
        }
        public static KhoaHocViewModel toJson(KhoaHoc obj)
        {
            KhoaHocViewModel view = new KhoaHocViewModel();
            view.MaKH = obj.MaKH;
            view.TenKH = obj.TenKH;
            view.MoTaKH = obj.MoTaKH;
            view.HinhAnh = obj.HinhAnh;
            view.LuotDK = obj.LuotDK;
            return view;
        }
        public static KhoaHocDKViewModel toJson(KhoaHocDK obj)
        {
            KhoaHocDKViewModel view = new KhoaHocDKViewModel();
            view.MaKH = obj.MaKH;
            view.MaKHDK = obj.MaKHDK;
            view.MaND = obj.MaND;
            return view;
        }
  
        public static DapAnViewModel toJson(DapAn obj)
        {
            DapAnViewModel view = new DapAnViewModel();
            view.MaDapAn = obj.MaDapAn;
            view.MaCauHoi = obj.MaCauHoi;
            view.NoiDungDapAn = obj.NoiDungDapAn;
            view.DapAnDung = obj.DapAnDung;
            return view;
        }
        public static CauHoiViewModel toJson(CauHoi obj)
        {
            CauHoiViewModel view = new CauHoiViewModel();
            view.MaCauHoi = obj.MaCauHoi;
            view.NoiDungCauHoi = obj.NoiDungCauHoi;
            view.TheLoai = obj.TheLoai;
            view.MaKT = obj.MaKT;
            return view;
        }
        public static BinhLuanViewModel toJson(BinhLuan obj)
        {
            BinhLuanViewModel view = new BinhLuanViewModel();
            view.MaBL = obj.MaBL;
            view.NoiDung = obj.NoiDung;
            view.MaBaiHoc = obj.MaBaiHoc;
            view.MaND = obj.MaND;
            view.NgayBL = obj.NgayBL;
            return view;
        }
        public static BaiHocViewModel toJson(BaiHoc obj)
        {
            BaiHocViewModel view = new BaiHocViewModel();
            view.MaBaiHoc = obj.MaBaiHoc;
            view.Video = obj.Video;
            view.GioiThieu = obj.GioiThieu;
            view.LyThuyet = obj.LyThuyet;
            view.CodeMau = obj.CodeMau;
            view.LuocHoc = obj.LuocHoc;
            view.MaKH = obj.MaKH;
            return view;
        }
    }
}