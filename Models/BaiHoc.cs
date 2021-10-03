namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BaiHoc")]
    public partial class BaiHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiHoc()
        {
            KiemTras = new HashSet<KiemTra>();
            BinhLuans = new HashSet<BinhLuan>();
            LuuBaiHocs = new HashSet<LuuBaiHoc>();
        }

        [Key]
        [StringLength(8)]
        public string MaBaiHoc { get; set; }

        [StringLength(200)]
        public string Video { get; set; }

        public string GioiThieu { get; set; }

        public string LyThuyet { get; set; }

        public string CodeMau { get; set; }

        [StringLength(10)]
        public string LuocHoc { get; set; }

        [StringLength(8)]
        public string MaKH { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KiemTra> KiemTras { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LuuBaiHoc> LuuBaiHocs { get; set; }
    }
}
