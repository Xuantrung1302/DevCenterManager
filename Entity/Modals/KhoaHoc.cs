using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class KhoaHoc
    {
      
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public decimal HocPhi { get; set; }
        public int HeSoLyThuyet { get; set; }
        public int HeSoThucHanh { get; set; }
        public int HeSoDuAn { get; set; }
        public int HeSoCuoiKy { get; set; }

        public ICollection<LopHoc> LopHocs { get; set; }
    }

}