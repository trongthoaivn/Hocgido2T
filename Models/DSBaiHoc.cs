namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DSBaiHoc")]
    public partial class DSBaiHoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DSBaiHoc()
        {
            BaiHocs = new HashSet<BaiHoc>();
        }

        [Key]
        [StringLength(8)]
        public string MaDS { get; set; }

        public int? SoBaiHoc { get; set; }

        [StringLength(100)]
        public string TenBaiHoc { get; set; }

        [StringLength(8)]
        public string MaKH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BaiHoc> BaiHocs { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}
