using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEduManager
{

    public static class Common
    {

        public static void LoadComboBoxLoaiHV(ComboBox cboLoaiHV)
        {
            List<Item> list = new List<Item>()
            {
                new Item{ Name = "Học viên tiềm năng", Value = "LHV00" },
                new Item{ Name = "Học viên chính thức", Value = "LHV01" }
            };
            cboLoaiHV.DataSource = list;
            cboLoaiHV.DisplayMember = "Name";
            cboLoaiHV.ValueMember = "Value";
        }
        public static void LoadComboBoxLoaiNV(ComboBox cboLoaiNV)
        {
            List<Item> list = new List<Item>()
            {
                new Item{ Name = "Quản trị viên", Value = "LNV00" },
                new Item{ Name = "Nhân viên ghi danh", Value = "LNV01" },
                new Item{ Name = "Nhân viên học vụ", Value = "LNV02" },
                new Item{ Name = "Nhân viên kế toán", Value = "LNV03" },
            };
            cboLoaiNV.DataSource = list;
            cboLoaiNV.DisplayMember = "Name";
            cboLoaiNV.ValueMember = "Value";
        }
    }

    public class Item
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
