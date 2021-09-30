namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        [StringLength(8)]
        public string MaBL { get; set; }

        public string NoiDung { get; set; }

        [StringLength(8)]
        public string MaND { get; set; }

        [StringLength(8)]
        public string MaBaiHoc { get; set; }

        public DateTime? NgayBL { get; set; }

        public virtual BaiHoc BaiHoc { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
