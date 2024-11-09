using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class LoaiNV
    {
        public string MaLoaiNV { get; set; }
        public string TenLoaiNV { get; set; }

        public ICollection<NhanVien> NhanViens { get; set; }
    }

}