using BusinessLogic;
using Enity.Models;
using Newtonsoft.Json;
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
    public partial class frmQuanLyKhoaHoc : Form
    {
        CallAPI callAPI = new CallAPI();

        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Course/";
        private string _url2 = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";

        private bool isInsert = false;
        public frmQuanLyKhoaHoc()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Khóa điều khiển các control
        /// </summary>
        public void LockPanelControl()
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            numHocPhi.Enabled = false;
            numDiemLT.Enabled = false;
            numDiemTH.Enabled = false;
            numDiemDA.Enabled = false;
            numDiemCK.Enabled = false;
            btnLuuThongTin.Enabled = false;
            btnHuyBo.Enabled = false;
        }

        /// <summary>
        /// Mở khóa điều khiển các control
        /// </summary>
        public void UnlockPanelControl()
        {
            txtTenKH.Enabled = true;
            numHocPhi.Enabled = true;
            numDiemLT.Enabled = true;
            numDiemTH.Enabled = true;
            numDiemDA.Enabled = true;
            numDiemCK.Enabled = true;
            btnLuuThongTin.Enabled = true;
            btnHuyBo.Enabled = true;
        }

        /// <summary>
        /// Đặt lại giá trị của các control trong panel
        /// </summary>
        public void ResetPanelControl()
        {
            txtMaKH.Text = string.Empty;
            txtTenKH.Text = string.Empty;
            numHocPhi.Value = 0;
            numDiemLT.Value = 0;
            numDiemTH.Value = 0;
            numDiemDA.Value = 0;
            numDiemCK.Value = 0;
        }


        /// <summary>
        /// Kiểm tra hợp lệ các hệ số
        /// </summary>
        public void ValidateHeSo()
        {
            if (numDiemDA.Value + numDiemLT.Value + numDiemTH.Value + numDiemCK.Value != 100)
                throw new ArgumentException("Tổng các hệ số điểm phải bằng 100%");
        }

        public async void LoadDataToGridView()
        {
            try
            {
                string url = $"{_url}thongTinKhoaHoc";
                DataTable result = await callAPI.GetAPI(url);
                gridKH.DataSource = result;
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void LoadData()
        {

        }
    

        #region Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridKH_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            lblTongCong.Text = string.Format("Tổng cộng: {0} khóa học", gridKH.Rows.Count);

        }

        private void gridKH_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            lblTongCong.Text = string.Format("Tổng cộng: {0} khóa học", gridKH.Rows.Count);

        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            UnlockPanelControl();
            isInsert = false;
            // Điều kiện khi thêm
            KhoaHoc khoaHoc = new KhoaHoc()
            {
                MaKH = txtMaKH.Text,
                TenKH = txtTenKH.Text,
                HocPhi = (decimal)numHocPhi.Value,
                HeSoLyThuyet = (int)numDiemLT.Value, // Chuyển đổi sang int
                HeSoThucHanh = (int)numDiemTH.Value,
                HeSoDuAn = (int)numDiemDA.Value,
                HeSoCuoiKy = (int)numDiemCK.Value
            };

            // Chuyển đổi đối tượng thành JSON
            string jsonData = JsonConvert.SerializeObject(khoaHoc);

            string url = $"{_url}suaThongTinKhoaHoc";
            bool result = await callAPI.PostAPI(url, jsonData);
            if (result)
            {
                MessageBox.Show("Sửa thông tin học viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Sửa thông tin học viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gridKH_DoubleClick(object sender, EventArgs e)
        {
            btnSua_Click(sender, e);
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Gọi API để tạo ID tự động
                //ngay={DateTime.Now.ToString("yyyy-MM-dd")}
                string url = $"{_url2}taoIdTuDong?ngay={DateTime.Now.Date.ToString("dd/MM/yyyy")}&ma=KH";
                string result = await callAPI.CallApiAsync(url);
                // Kiểm tra kết quả trả về từ API
                if (!string.IsNullOrEmpty(result))
                {
                    // Mở khóa các control và đặt ID tự động cho txtMaKH
                    UnlockPanelControl();
                    ResetPanelControl();
                    txtMaKH.Text = result.Trim('"');
                    isInsert = true;
                }
                else
                {
                    MessageBox.Show("Không thể tạo ID tự động. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateHeSo();

                KhoaHoc khoaHoc = new KhoaHoc()
                {
                    MaKH = txtMaKH.Text,
                    TenKH = txtTenKH.Text,
                    HocPhi = numHocPhi.Value,
                    HeSoLyThuyet = (int)numDiemLT.Value,
                    HeSoThucHanh = (int)numDiemTH.Value,
                    HeSoDuAn = (int)numDiemDA.Value,
                    HeSoCuoiKy = (int)numDiemCK.Value
                };

                string jsonData = JsonConvert.SerializeObject(khoaHoc);
                string url = isInsert ? $"{_url}themThongTinKhoaHoc" : $"{_url}suaThongTinKhoaHoc";

                bool result = await callAPI.PostAPI(url, jsonData);
                if (result)
                {
                    MessageBox.Show(isInsert ? "Thêm thông tin học viên thành công" : "Sửa thông tin học viên thành công",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataToGridView();
                    ResetPanelControl();
                    LockPanelControl();
                }
                else
                {
                    MessageBox.Show(isInsert ? "Thêm thông tin học viên không thành công" : "Sửa thông tin học viên không thành công",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {

        }

        private void frmQuanLyKhoaHoc_Load(object sender, EventArgs e)
        {
            
            gridKH.AutoGenerateColumns = false;
            LockPanelControl();
            btnLuuThongTin.Enabled = false;
            btnHuyBo.Enabled = false;
            LoadDataToGridView();
        }

        private void gridKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Đổ dữ liệu vào các ô bên trái từ dòng được chọn trong grid
                txtMaKH.Text = gridKH.Rows[e.RowIndex].Cells["clmMaKH"].Value.ToString();
                txtTenKH.Text = gridKH.Rows[e.RowIndex].Cells["clmTenKH"].Value.ToString();
                numHocPhi.Value = Convert.ToDecimal(gridKH.Rows[e.RowIndex].Cells["clmHocPhi"].Value);
                numDiemLT.Value = Convert.ToInt32(gridKH.Rows[e.RowIndex].Cells["clmHSLyThuyet"].Value);
                numDiemTH.Value = Convert.ToInt32(gridKH.Rows[e.RowIndex].Cells["clmHSThucHanh"].Value);
                numDiemDA.Value = Convert.ToInt32(gridKH.Rows[e.RowIndex].Cells["clmHSDuAn"].Value);
                numDiemCK.Value = Convert.ToInt32(gridKH.Rows[e.RowIndex].Cells["clmHSCuoiKy"].Value);

                // Khóa các control và chỉ bật các nút Thêm, Sửa, Xóa
                LockPanelControl();
                btnLuuThongTin.Enabled = false;
                btnHuyBo.Enabled = false;
            }
        }

    }
    #endregion
}
