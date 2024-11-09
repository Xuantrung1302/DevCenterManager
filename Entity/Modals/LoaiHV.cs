using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class LoaiHV
    {

        public string MaLoaiHV { get; set; }
        public string TenLoaiHV { get; set; }

        public ICollection<HocVien> HocViens { get; set; }
    }

}