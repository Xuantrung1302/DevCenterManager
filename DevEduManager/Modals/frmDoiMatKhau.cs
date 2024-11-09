using BusinessLogic;
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
    public partial class frmDoiMatKhau : Form
    {
        private string _userName;
        private string _userFullName;
        private string _password;
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";
        public frmDoiMatKhau(string userFullName, string userName, string password)
        {
            InitializeComponent();
            _userFullName = userFullName;
            _userName = userName;
            _password = password;
        }
        private bool CheckDoiMatKhau()
        {
            if (txtMatKhauCu.Text == "")
            {
                errorProvider1.SetError(txtMatKhauCu, "Bạn chưa nhập mật khẩu cũ");
                txtMatKhauCu.Focus();
                return false;
            }
            else if (txtMatKhauMoi.Text == "")
            {
                errorProvider1.SetError(txtMatKhauMoi, "Bạn chưa nhập mật khẩu mới");
                txtMatKhauMoi.Focus();
                return false;
            }
            else if (txtMatKhauMoiAgain.Text == "")
            {
                errorProvider1.SetError(txtMatKhauMoiAgain, "Bạn chưa nhập mật khẩu mới lần hai");
                txtMatKhauMoiAgain.Focus();
                return false;
            }

            return true;
        }
        private void Refresh()
        {
            txtMatKhauCu.Text = "";
            txtMatKhauMoi.Text = "";
            txtMatKhauMoiAgain.Text = "";

        }
        #region Events
        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            lblUserName.Text = _userFullName;

            txtTenDangNhap.Text = _userName;
        }

        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra điều kiện thay đổi mật khẩu trước khi gọi API
                if (CheckDoiMatKhau())
                {
                    errorProvider1.Clear();
                    // Kiểm tra mật khẩu cũ có khớp với mật khẩu hiện tại không
                    if (txtMatKhauCu.Text == _password)
                    {
                        // Kiểm tra mật khẩu mới không rỗng và khớp nhau
                        if (!string.IsNullOrEmpty(txtMatKhauMoi.Text) && txtMatKhauMoi.Text == txtMatKhauMoiAgain.Text)
                        {
                            string tenDangNhap = txtTenDangNhap.Text;
                            string matKhauMoi = txtMatKhauMoi.Text;


                            // Tạo đối tượng JSON để gửi lên API
                            var accountData = new
                            {
                                TenDangNhap = tenDangNhap,
                                MatKhau = matKhauMoi

                            };

                            // Chuyển đổi đối tượng thành JSON
                            string jsonData = JsonConvert.SerializeObject(accountData);
                            // Gọi API để thay đổi mật khẩu
                            string url = $"{_url}doiMatKhau";
                            bool result = await callAPI.PostAPI(url, jsonData);

                            // Kiểm tra kết quả từ API
                            if (result)
                            {
                                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();  // Đóng form sau khi đổi mật khẩu thành công
                            }
                            else
                            {
                                MessageBox.Show("Đổi mật khẩu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Refresh(); // Làm mới giao diện hoặc thực hiện lại thao tác
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu mới trống hoặc không khớp", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu cũ không chính xác", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #endregion
    }
}
