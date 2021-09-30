namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhoaHocDK")]
    public partial class KhoaHocDK
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string MaKH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string MaND { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(8)]
        public string MaKHDK { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
