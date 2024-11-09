using BusinessLogic;
using Enity.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace DevEduManager.Modals
{
    public partial class frmThongTinTrungTam : Form
    {
        public frmThongTinTrungTam()
        {
            InitializeComponent();
        }
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";
        #region Events

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmThongTinTrungTam_Load(object sender, EventArgs e)
        {
            string url = $"{_url}thongTinTrungTam";
            DataTable result = await callAPI.GetAPI(url);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                txtTenTrungTam.Text = row["TenTT"].ToString();
                txtDiaChi.Text = row["DiaChiTT"].ToString();
                txtSDT.Text = row["SdtTT"].ToString(); ;
                txtEmail.Text = row["EmailTT"].ToString(); ;
                txtWebsite.Text = row["Website"].ToString();
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
                string tenTrungTam = txtTenTrungTam.Text;
                string diaChi = txtDiaChi.Text;
                string sdtTT = txtSDT.Text;
                string email = txtEmail.Text;
                string website = txtWebsite.Text;

                //// Tạo đối tượng JSON để gửi lên API
                //var accountData = new
                //{
                //    TenTT = tenTrungTam,
                //    MatKhau = matKhauMoi

                //};
                ChiTietTrungTam chiTietTrungTam = new ChiTietTrungTam()
                {
                    TenTT = tenTrungTam,
                    DiaChiTT = diaChi,
                    SdtTT = sdtTT,
                    EmailTT = email,
                    Website = website
                };

                // Chuyển đổi đối tượng thành JSON
                string jsonData = JsonConvert.SerializeObject(chiTietTrungTam);
                // Gọi API để thay đổi mật khẩu
                string url = $"{_url}suaThongTinTrungTam";
                bool result = await callAPI.PostAPI(url, jsonData);

                // Kiểm tra kết quả từ API
                if (result)
                {
                    MessageBox.Show("Sửa thông tin trung tâm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();  // Đóng form sau khi đổi mật khẩu thành công
                }
                else
                {
                    MessageBox.Show("Sửa thông tin trung tâm không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Refresh(); // Làm mới giao diện hoặc thực hiện lại thao tác
                }
                this.Close();
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion


    }
}
