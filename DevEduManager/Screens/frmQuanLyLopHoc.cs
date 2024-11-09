using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEduManager.Screens
{
    public partial class frmQuanLyLopHoc : Form
    {
        public frmQuanLyLopHoc()
        {
            InitializeComponent();
        }

        // <summary>
        /// Kiểm tra nhập liệu tìm kiếm có hợp lệ
        /// </summary>
        public void ValidateSearch()
        {
            if (chkMaLop.Checked && txtMaLop.Text == string.Empty)
                throw new ArgumentException("Mã lớp không được trống");
            if (chkTenLop.Checked && txtTenLop.Text == string.Empty)
                throw new ArgumentException("Tên lớp không được trống");
        }

        #region
        private void frmQuanLyLopHoc_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //frmLopHocEdit frm = new frmLopHocEdit();
            //frm.Text = "Thêm lớp mới";
            //frm.ShowDialog();

            //btnHienTatCa_Click(sender, e);
        }

        private void chkMaLop_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkTenLop_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkKhoa_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkKhoangTG_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkTinhTrang_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
