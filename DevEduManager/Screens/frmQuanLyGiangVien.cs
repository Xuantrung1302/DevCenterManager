using BusinessLogic;
using DevEduManager.Modals;
using Enity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEduManager.Screens
{
    public partial class frmQuanLyGiangVien : Form
    {
        public frmQuanLyGiangVien()
        {
            InitializeComponent();
        }
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Teacher/";

        /// <summary>
        /// Kiểm tra nhập liệu tìm kiếm có hợp lệ
        /// </summary>
        public void ValidateSearch()
        {
            if (chkMaGV.Checked && txtMaGV.Text == string.Empty)
                throw new ArgumentException("Mã giảng viên không được trống");
            if (chkTenGV.Checked && txtTenGV.Text == string.Empty)
                throw new ArgumentException("Họ và tên giảng viên không được trống");
        }

        private async void LoadDataToGridView(string maGV = null, string tenGV = null, string gioiTinh = null)
        {
            string url = $"{_url}thongTinGiangVien?maGV={maGV}&tenGV={tenGV}&gioiTinh={gioiTinh}";
            DataTable result = await callAPI.GetAPI(url);

            gridGV.Dock = DockStyle.Fill;

            if (result.Rows.Count > 0)
            {
                gridGV.DataSource = result;
            }

        }

        #region Events
        private void chkMaGV_CheckedChanged(object sender, EventArgs e)
        {
            txtMaGV.Enabled = chkMaGV.Checked;
        }

        private void chkTenGV_CheckedChanged(object sender, EventArgs e)
        {
            txtTenGV.Enabled = chkTenGV.Checked;
        }

        private void chkGioiTinh_CheckedChanged(object sender, EventArgs e)
        {
            cboGioiTinh.Enabled = chkGioiTinh.Checked;
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            chkMaGV.Checked = true;
            chkTenGV.Checked = chkGioiTinh.Checked = false;
            txtMaGV.Text = txtTenGV.Text = string.Empty;
            cboGioiTinh.SelectedIndex = 0;
        }

        private void frmQuanLyGiangVien_Load(object sender, EventArgs e)
        {
            btnDatLai_Click(sender, e);
            btnHienTatCa_Click(sender, e);
            gridGV_Click(sender, e);
            LoadDataToGridView(null,null,null);
        }

        private void btnHienTatCa_Click(object sender, EventArgs e)
        {
            LoadDataToGridView(null, null, null);
        }

        private void gridGV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblTongCongGV.Text = string.Format("Tổng cộng: {0} giảng viên", gridGV.Rows.Count);
        }

        private void gridGV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTongCongGV.Text = string.Format("Tổng cộng: {0} giảng viên", gridGV.Rows.Count);
        }

        private void gridGV_DoubleClick(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //frmGiangVienEdit frm = new frmGiangVienEdit();
            //frm.Text = "Cập nhật thông tin giảng viên";
            //frm.ShowDialog();

            if (gridGV.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu từ hàng đang được chọn
                DataGridViewRow selectedRow = gridGV.SelectedRows[0];
                DataTable dt = ((DataTable)gridGV.DataSource).Clone();  // Tạo bản sao cấu trúc của DataTable
                DataRow row = dt.NewRow();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    row[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(row);

                // Mở form sửa thông tin giáo viên
                frmGiangVienEdit frm = new frmGiangVienEdit(dt);
                frm.Text = "Cập nhật thông tin học viên";
                frm.ShowDialog();

                // Tải lại danh sách sau khi chỉnh sửa
                btnHienTatCa_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một học viên để sửa.");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmGiangVienEdit frm = new frmGiangVienEdit(null); // Gửi null cho form khi thêm mới
            frm.Text = "Thêm giảng viên mới";
            frm.ShowDialog();

            // Tải lại danh sách sau khi thêm
            btnHienTatCa_Click(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void gridLop_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblTongCongLop.Text = string.Format("Tổng cộng: {0} lớp", gridLop.Rows.Count);
        }

        private void gridLop_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTongCongLop.Text = string.Format("Tổng cộng: {0} lớp", gridLop.Rows.Count);
        }

        private void gridGV_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSearch();

                string maGV = chkMaGV.Checked ? txtMaGV.Text.Trim() : null;
                string tenGV = chkTenGV.Checked ? txtTenGV.Text.Trim() : null;
                string gioiTinhText = chkGioiTinh.Checked ? cboGioiTinh.Text : null;

                // Gọi LoadDataToGridView với tham số cần thiết
                LoadDataToGridView(maGV, tenGV, gioiTinhText);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
