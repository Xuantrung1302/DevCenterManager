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

namespace DevEduManager.Modals
{
    public partial class frmThayDoiThongTinHV : Form
    {
        string _maHV;
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Students/";
        public frmThayDoiThongTinHV(string maHV)
        {
            InitializeComponent();
            _maHV = maHV;
        }

        /// <summary>
        /// Kiểm tra hợp lệ
        /// </summary>
        public void ValidateLuu()
        {
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
                throw new ArgumentException("Địa chỉ không được trống");
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
                throw new ArgumentException("Số điện thoại không được trống");
        }


        #region Events

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmThayDoiThongTinHV_Load(object sender, EventArgs e)
        {
            try
            {
                string url = $"{_url}thongTinHocVien?MaHV={_maHV}";

                DataTable result = await callAPI.GetAPI(url);

                // Kiểm tra nếu có dữ liệu trả về
                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];

                    // Điền thông tin nhân viên vào các trường TextBox
                    txtMaHV.Text = row["MaHV"].ToString();
                    txtTenHV.Text = row["TenHV"].ToString();
                    txtSDT.Text = row["SdtHV"].ToString();
                    txtEmail.Text = row["EmailHV"].ToString();
                    txtDiaChi.Text = row["DiaChi"].ToString();
                    cboGioiTinh.SelectedItem = row["GioiTinhHV"].ToString();
                    dateNgaySinh.Value = Convert.ToDateTime(row["NgaySinh"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin học viên: {ex.Message}");
            }
        }


        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateLuu();

                HocVien hocVien = new HocVien()
                {
                    MaHV = txtMaHV.Text,
                    TenHV = txtTenHV.Text,
                    GioiTinhHV = cboGioiTinh.SelectedItem.ToString(), // Gán cho GioiTinhHV thay vì NgaySinh
                    NgaySinh = DateTime.Parse(dateNgaySinh.Value.ToString()), // Chuyển đổi giá trị từ DateTimePicker
                    DiaChi = txtDiaChi.Text,
                    SdtHV = txtSDT.Text,
                    EmailHV = txtEmail.Text
                };


                // Chuyển đổi đối tượng thành JSON
                string jsonData = JsonConvert.SerializeObject(hocVien);
                // Gọi API để thay đổi mật khẩu
                string url = $"{_url}suaThongTinHocVien";
                bool result = await callAPI.PostAPI(url, jsonData);
                if (result)
                {
                    MessageBox.Show("Cập nhật thông tin học viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin học viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        #endregion


    }
}
