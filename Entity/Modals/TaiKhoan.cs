using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class TaiKhoan
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }

        public ICollection<HocVien> HocViens { get; set; }
        public ICollection<NhanVien> NhanViens { get; set; }
        public ICollection<GiangVien> GiangViens { get; set; }
    }

}