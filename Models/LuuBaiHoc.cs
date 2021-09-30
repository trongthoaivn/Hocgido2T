namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LuuBaiHoc")]
    public partial class LuuBaiHoc
    {
        [Key]
        [StringLength(8)]
        public string MaLuu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayLuu { get; set; }

        [StringLength(8)]
        public string MaBaiHoc { get; set; }

        [StringLength(8)]
        public string MaND { get; set; }

        public virtual BaiHoc BaiHoc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
