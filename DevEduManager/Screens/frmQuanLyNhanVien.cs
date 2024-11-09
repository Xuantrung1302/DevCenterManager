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
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEduManager.Screens
{
    //string maHocVien = "LNV00";


    public partial class frmQuanLyNhanVien : Form
    {
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";
        private string _url2 = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Employee/";
        public frmQuanLyNhanVien()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Kiểm tra nhập liệu tìm kiếm có hợp lệ
        /// </summary>
        public void ValidateSearch()
        {
            if (chkMaNV.Checked && txtMaNV.Text == string.Empty)
                throw new ArgumentException("Mã nhân viên không được trống");
            if (chkTenNV.Checked && txtTenNV.Text == string.Empty)
                throw new ArgumentException("Họ và tên nhân viên không được trống");
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmNhanVienEdit frm = new frmNhanVienEdit(null);
            frm.Text = "Thêm nhân viên mới";
            frm.ShowDialog();

            btnHienTatCa_Click(sender, e);
        }

        private void chkMaNV_CheckedChanged(object sender, EventArgs e)
        {
            txtMaNV.Enabled = chkMaNV.Checked;
        }

        private void chkTenNV_CheckedChanged(object sender, EventArgs e)
        {
            txtTenNV.Enabled = chkTenNV.Checked;
        }

        private void chkLoaiNV_CheckedChanged(object sender, EventArgs e)
        {
            cboLoaiNV.Enabled = chkLoaiNV.Checked;
        }

        private void btnDatLai_Click(object sender, EventArgs e)
        {
            chkMaNV.Checked = true;
            txtMaNV.Text = txtTenNV.Text = string.Empty;
            chkTenNV.Checked = chkLoaiNV.Checked = false;
        }
        private async void LoadDataToGridView(string maNV = null, string tenNV = null, string tenLoaiNV = null)
        {
            try
            {
                string url = $"{_url2}thongTinNhanVien";
                DataTable result = await callAPI.GetAPI(url);

                // Lọc dữ liệu dựa trên tham số tìm kiếm
                var filteredRows = from row in result.AsEnumerable()
                                   where (string.IsNullOrEmpty(tenLoaiNV) || row.Field<string>("TenLoaiNV").Contains(tenLoaiNV)) &&
                                         (string.IsNullOrEmpty(maNV) || row.Field<string>("MaNV").Contains(maNV)) &&
                                         (string.IsNullOrEmpty(tenNV) || row.Field<string>("TenNV").ToLower().Contains(tenNV.ToLower()))
                                   select row;

                // Nếu có kết quả lọc, hiển thị trên grid
                if (filteredRows.Any())
                {
                    DataTable filteredDataTable = filteredRows.CopyToDataTable();
                    gridNV.DataSource = filteredDataTable;
                    gridNV.Dock = DockStyle.Fill;

                    // Ẩn các cột cuối nếu cần thiết
                    if (gridNV.Rows.Count > 0)
                    {
                        int columnCount = gridNV.Columns.Count;
                        if (columnCount >= 5)
                        {
                            gridNV.Columns[columnCount - 1].Visible = false; // Cột cuối

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy kết quả phù hợp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void frmQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            //load loại nhân viên
            //cboLoaiNV.DataSource = LoaiNV.SelectAll();
            cboLoaiNV.DisplayMember = "TenLoaiNV";
            cboLoaiNV.ValueMember = "MaLoaiNV";
            Common.LoadComboBoxLoaiNV(cboLoaiNV);
            LoadDataToGridView();
            //cboLoaiNV.SelectedIndexChanged += new EventHandler(cboLoaiNV_SelectedIndexChanged);
            btnDatLai_Click(sender, e);
            LoadDataToGridView();
            btnHienTatCa_Click(sender, e);
        }

        private void btnHienTatCa_Click(object sender, EventArgs e)
        {
            LoadDataToGridView();
        }

        private void gridNV_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblTongCong.Text = string.Format("Tổng cộng: {0} nhân viên", gridNV.Rows.Count);
        }

        private void gridNV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTongCong.Text = string.Format("Tổng cộng: {0} nhân viên", gridNV.Rows.Count);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateSearch();

                string maNV = chkMaNV.Checked ? txtMaNV.Text.Trim() : null;
                string tenNV = chkTenNV.Checked ? txtTenNV.Text.Trim() : null;
                string loaiNVText = chkLoaiNV.Checked ? cboLoaiNV.Text : null;

                // Gọi LoadDataToGridView với tham số cần thiết
                LoadDataToGridView(maNV, tenNV, loaiNVText);
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (gridNV.SelectedRows.Count > 0)
            {
                // Lấy dữ liệu từ hàng đang được chọn
                DataGridViewRow selectedRow = gridNV.SelectedRows[0];
                DataTable dt = ((DataTable)gridNV.DataSource).Clone();  // Tạo bản sao cấu trúc của DataTable
                DataRow row = dt.NewRow();

                foreach (DataGridViewCell cell in selectedRow.Cells)
                {
                    row[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(row);

                // Mở form sửa thông tin nhân viên
                frmNhanVienEdit frm = new frmNhanVienEdit(dt);
                frm.Text = "Cập nhật thông tin nhân viên";
                frm.ShowDialog();

                // Tải lại danh sách sau khi chỉnh sửa
                btnHienTatCa_Click(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một nhân viên để sửa.");
            }
        }

        private void gridNV_DoubleClick(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }
    }
}
