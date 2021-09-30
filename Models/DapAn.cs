namespace Hocgido2T.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DapAn")]
    public partial class DapAn
    {
        [Key]
        [StringLength(8)]
        public string MaDapAn { get; set; }

        public string NoiDungDapAn { get; set; }

        public bool? DapAnDung { get; set; }

        [StringLength(8)]
        public string MaCauHoi { get; set; }

        public virtual CauHoi CauHoi { get; set; }
    }
}
