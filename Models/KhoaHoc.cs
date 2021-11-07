namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhoaHoc")]
    public partial class KhoaHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhoaHoc()
        {
            BaiHocs = new HashSet<BaiHoc>();
            KhoaHocDKs = new HashSet<KhoaHocDK>();
        }

        [Key]
        [StringLength(8)]
        public string MaKH { get; set; }

        [StringLength(50)]
        public string TenKH { get; set; }

        public string MoTaKH { get; set; }

        public string HinhAnh { get; set; }

        public int? LuotDK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiHoc> BaiHocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KhoaHocDK> KhoaHocDKs { get; set; }
    }
}
