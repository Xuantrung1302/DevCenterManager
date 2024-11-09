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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevEduManager.Modals
{
    public partial class frmThayDoiThongTinGV : Form
    {
        string _maGV;
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Teacher/";
        public frmThayDoiThongTinGV(string maHV)
        {
            InitializeComponent();
            _maGV = maHV;
        }
        /// <summary>
        /// Kiểm tra hợp lệ
        /// </summary>
        public void ValidateLuu()
        {
            if (string.IsNullOrWhiteSpace(txtTenGV.Text))
                throw new ArgumentException("Địa chỉ không được trống");
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
                throw new ArgumentException("Số điện thoại không được trống");
        }


        #region Events
        private async void frmThayDoiThongTinGV_Load(object sender, EventArgs e)
        {
            try
            {
                string url = $"{_url}thongTinGiangVien?Maga={_maGV}";

                DataTable result = await callAPI.GetAPI(url);

                // Kiểm tra nếu có dữ liệu trả về
                if (result.Rows.Count > 0)
                {
                    DataRow row = result.Rows[0];

                    // Điền thông tin nhân viên vào các trường TextBox
                    txtMaGV.Text = row["MaGV"].ToString();
                    txtTenGV.Text = row["TenGV"].ToString();
                    txtSDT.Text = row["SdtGV"].ToString();
                    cboGioiTinh.SelectedItem = row["GioiTinhGV"].ToString();
                    txtEmail.Text = row["EmailGV"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin học viên: {ex.Message}");
            }
        }

        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateLuu();

                GiangVien giangVien = new GiangVien()
                {
                    //MaHV = txtMaHV.Text,
                    //TenHV = txtTenHV.Text,
                    //GioiTinhHV = cboGioiTinh.SelectedItem.ToString(), // Gán cho GioiTinhHV thay vì NgaySinh
                    //NgaySinh = DateTime.Parse(dateNgaySinh.Value.ToString()), // Chuyển đổi giá trị từ DateTimePicker
                    //DiaChi = txtDiaChi.Text,
                    //SdtHV = txtSDT.Text,
                    //EmailHV = txtEmail.Text,
                    MaGV = txtMaGV.Text,
                    SdtGV = txtSDT.Text,
                    EmailGV = txtEmail.Text,
                };


                // Chuyển đổi đối tượng thành JSON
                string jsonData = JsonConvert.SerializeObject(giangVien);
                // Gọi API để thay đổi mật khẩu
                string url = $"{_url}suaThongTinGiangVien";
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

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboGioiTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion


    }
}
