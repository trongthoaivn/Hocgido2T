using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Hocgido2T.Models
{
    public partial class dbhocgido : DbContext
    {
        public dbhocgido()
            : base("name=dbhocgido")
        {
        }

        public virtual DbSet<BaiHoc> BaiHocs { get; set; }
        public virtual DbSet<BinhLuan> BinhLuans { get; set; }
        public virtual DbSet<CauHoi> CauHois { get; set; }
        public virtual DbSet<DapAn> DapAns { get; set; }
        public virtual DbSet<KhoaHoc> KhoaHocs { get; set; }
        public virtual DbSet<KhoaHocDK> KhoaHocDKs { get; set; }
        public virtual DbSet<KiemTra> KiemTras { get; set; }
        public virtual DbSet<LuuBaiHoc> LuuBaiHocs { get; set; }
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<TinhTrangBH> TinhTrangBHs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaiHoc>()
                .Property(e => e.MaBaiHoc)
                .IsUnicode(false);

            modelBuilder.Entity<BaiHoc>()
                .Property(e => e.LuocHoc)
                .IsUnicode(false);

            modelBuilder.Entity<BaiHoc>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.MaBL)
                .IsUnicode(false);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.MaND)
                .IsUnicode(false);

            modelBuilder.Entity<BinhLuan>()
                .Property(e => e.MaBaiHoc)
                .IsUnicode(false);

            modelBuilder.Entity<CauHoi>()
                .Property(e => e.MaCauHoi)
                .IsUnicode(false);

            modelBuilder.Entity<CauHoi>()
                .Property(e => e.MaKT)
                .IsUnicode(false);

            modelBuilder.Entity<DapAn>()
                .Property(e => e.MaDapAn)
                .IsUnicode(false);

            modelBuilder.Entity<DapAn>()
                .Property(e => e.MaCauHoi)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHoc>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHoc>()
                .HasMany(e => e.KhoaHocDKs)
                .WithRequired(e => e.KhoaHoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhoaHocDK>()
                .Property(e => e.MaKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHocDK>()
                .Property(e => e.MaND)
                .IsUnicode(false);

            modelBuilder.Entity<KhoaHocDK>()
                .Property(e => e.MaKHDK)
                .IsUnicode(false);

            modelBuilder.Entity<KiemTra>()
                .Property(e => e.MaKT)
                .IsUnicode(false);

            modelBuilder.Entity<KiemTra>()
                .Property(e => e.MaBaiHoc)
                .IsUnicode(false);

            modelBuilder.Entity<LuuBaiHoc>()
                .Property(e => e.MaLuu)
                .IsUnicode(false);

            modelBuilder.Entity<LuuBaiHoc>()
                .Property(e => e.MaBaiHoc)
                .IsUnicode(false);

            modelBuilder.Entity<LuuBaiHoc>()
                .Property(e => e.MaND)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MaND)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .Property(e => e.MaTK)
                .IsUnicode(false);

            modelBuilder.Entity<NguoiDung>()
                .HasMany(e => e.KhoaHocDKs)
                .WithRequired(e => e.NguoiDung)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MaTK)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.Quyen)
                .IsUnicode(false);

            modelBuilder.Entity<TinhTrangBH>()
                .Property(e => e.MaTTBH)
                .IsUnicode(false);

            modelBuilder.Entity<TinhTrangBH>()
                .Property(e => e.MaKHDDK)
                .IsUnicode(false);

            modelBuilder.Entity<TinhTrangBH>()
                .Property(e => e.MaND)
                .IsUnicode(false);
        }
    }
}
