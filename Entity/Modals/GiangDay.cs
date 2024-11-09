using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;

namespace Enity.Models
{
    public class GiangDay
    {

        public string MaGV { get; set; }
        public string MaLop { get; set; }

        // Foreign keys
        public GiangVien GiangVien { get; set; }
        public LopHoc LopHoc { get; set; }
    }

}