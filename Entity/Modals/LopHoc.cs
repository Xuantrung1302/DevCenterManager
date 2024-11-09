using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class LopHoc
    {
        public string MaLop { get; set; }
        public string TenLop { get; set; }
        public DateTime NgayBD { get; set; }
        public DateTime NgayKT { get; set; }
        public int SiSo { get; set; }
        public bool DangMo { get; set; }

        // Foreign key
        public string MaKH { get; set; }
        public KhoaHoc KhoaHoc { get; set; }
    }

}