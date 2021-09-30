namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHoi")]
    public partial class CauHoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CauHoi()
        {
            DapAns = new HashSet<DapAn>();
        }

        [Key]
        [StringLength(8)]
        public string MaCauHoi { get; set; }

        public string NoiDungCauHoi { get; set; }

        [StringLength(30)]
        public string TheLoai { get; set; }

        [StringLength(8)]
        public string MaKT { get; set; }

        public virtual KiemTra KiemTra { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DapAn> DapAns { get; set; }
    }
}
