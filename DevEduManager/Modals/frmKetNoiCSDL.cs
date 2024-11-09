using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DevEduManager.Modals
{
    public partial class frmKetNoiCSDL : Form
    {
        private string connectionString;

        public frmKetNoiCSDL()
        {
            InitializeComponent();
        }

        #region Events

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenServer.Text))
                    throw new ArgumentException("Tên máy chủ không được để trống");

                connectionString = string.Format("Data Source={0};Initial Catalog=master;", txtTenServer.Text);

                if (cboKieuXacThuc.SelectedIndex == 0)
                    connectionString += "Integrated Security=True;";
                else
                {
                    if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text) || string.IsNullOrWhiteSpace(txtMatKhau.Text))
                        throw new ArgumentException("Tên đăng nhập và mật khẩu không được để trống");

                    connectionString += string.Format("User Id={0};Password={1};", txtTenDangNhap.Text, txtMatKhau.Text);
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    DataTable dt = conn.GetSchema("Databases");
                    cboDatabase.DataSource = dt;
                    cboDatabase.DisplayMember = "database_name";
                    cboDatabase.ValueMember = "database_name";
                }

                MessageBox.Show("Kết nối thành công!" + Environment.NewLine + "Vui lòng chọn cơ sở dữ liệu trong danh sách bên dưới.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboDatabase.Enabled = true;
                btnLuuThongTin.Enabled = true;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại! " + ex.Message, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmKetNoiCSDL_Load(object sender, EventArgs e)
        {
            cboKieuXacThuc.Items.AddRange(new string[]
            {
                "Xác thực của Windows",
                "Xác thực của SQL Server"
            });
            cboKieuXacThuc.SelectedIndex = 0;

            cboDatabase.Enabled = false;
            btnLuuThongTin.Enabled = false;
        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            connectionString = string.Format("Data Source={0};Initial Catalog={1};", txtTenServer.Text, cboDatabase.SelectedValue);

            if (cboKieuXacThuc.SelectedIndex == 0)
                connectionString += "Integrated Security=True;";
            else
                connectionString += string.Format("User Id={0};Password={1};", txtTenDangNhap.Text, txtMatKhau.Text);

            // Lưu chuỗi kết nối vào app.config
            SaveConnectionStringToConfig(connectionString);

            MessageBox.Show("Đã lưu cài đặt kết nối cơ sở dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cboKieuXacThuc_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtTenDangNhap.Enabled = txtMatKhau.Enabled = cboKieuXacThuc.SelectedIndex == 1;
        }

        #endregion

        private void SaveConnectionStringToConfig(string connectionString)
        {
            // Mở file cấu hình
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Cập nhật chuỗi kết nối
            config.ConnectionStrings.ConnectionStrings["DataBaseConnection"].ConnectionString = connectionString;

            // Lưu file cấu hình
            config.Save(ConfigurationSaveMode.Modified);

            // Reload lại cấu hình để lấy giá trị mới
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}
