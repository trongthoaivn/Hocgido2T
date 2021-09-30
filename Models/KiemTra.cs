namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KiemTra")]
    public partial class KiemTra
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KiemTra()
        {
            CauHois = new HashSet<CauHoi>();
        }

        [Key]
        [StringLength(8)]
        public string MaKT { get; set; }

        [StringLength(8)]
        public string MaBaiHoc { get; set; }

        public virtual BaiHoc BaiHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoi> CauHois { get; set; }
    }
}
