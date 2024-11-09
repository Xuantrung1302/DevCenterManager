using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class DangKy
    {

        public string MaHV { get; set; }
        public string MaKH { get; set; }
        public string MaPhieu { get; set; }

        // Foreign keys
        public HocVien HocVien { get; set; }
        public KhoaHoc KhoaHoc { get; set; }
        public PhieuGhiDanh PhieuGhiDanh { get; set; }
    }

}