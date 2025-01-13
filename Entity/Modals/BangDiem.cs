using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class BangDiem
    {
        public string MaHV { get; set; }
        public string MaLop { get; set; }
        public string MaPhieu { get; set; }
        public int DiemLyThuyet { get; set; }
        public int DiemThucHanh { get; set; }
        public int DiemDuAn { get; set; }
        public int DiemCuoiKy { get; set; }

        // Foreign keys
        public HocVien HocVien { get; set; }
        public LopHoc LopHoc { get; set; }
        public PhieuGhiDanh PhieuGhiDanh { get; set; }
    }

}