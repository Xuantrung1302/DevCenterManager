using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class PhieuGhiDanh
    {
        public string MaPhieu { get; set; }
        public DateTime NgayGhiDanh { get; set; }
        public decimal DaDong { get; set; }
        public decimal ConNo { get; set; }

        // Foreign key
        public string MaNV { get; set; }
        public NhanVien NhanVien { get; set; }
    }

}