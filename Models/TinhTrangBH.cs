namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TinhTrangBH")]
    public partial class TinhTrangBH
    {
        [Key]
        [StringLength(8)]
        public string MaTTBH { get; set; }

        [StringLength(8)]
        public string MaKHDDK { get; set; }

        [StringLength(8)]
        public string MaND { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
