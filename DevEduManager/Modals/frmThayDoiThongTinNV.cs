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
    public partial class frmThayDoiThongTinNV : Form
    {
        string _maNV;
        public frmThayDoiThongTinNV(string ma)
        {
            InitializeComponent();
            _maNV = ma;
        }

        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Employee/";

        /// <summary>
        /// Kiểm tra hợp lệ
        /// </summary>
        public void ValidateLuu()
        {
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
                throw new ArgumentException("Số điện thoại không được trống");
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
                throw new ArgumentException("Email không được trống");
        }


        #region Events

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async void frmThayDoiThongTinNV_Load(object sender, EventArgs e)
        {
            string url = $"{_url}thongTinNhanVien?maNV={_maNV}";

            DataTable result = await callAPI.GetAPI(url);

            cboLoaiNV.DataSource = result; 
            cboLoaiNV.DisplayMember = "TenLoaiNV";
            cboLoaiNV.ValueMember = "MaLoaiNV";

            // Kiểm tra nếu có dữ liệu trả về
            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];

                // Điền thông tin nhân viên vào các trường TextBox
                txtMaNV.Text = row["MaNV"].ToString();
                txtTenNV.Text = row["TenNV"].ToString();
                txtSDT.Text = row["SdtNV"].ToString();
                txtEmail.Text = row["EmailNV"].ToString();


                // Thiết lập giá trị cho ComboBox Loại Nhân Viên
                cboLoaiNV.SelectedValue = row["MaLoaiNV"].ToString();
            }
        }


        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateLuu();

                NhanVien nhanVien = new NhanVien()
                {
                    MaNV = txtMaNV.Text,
                    TenNV = txtTenNV.Text,
                    SdtNV = txtSDT.Text,
                    EmailNV = txtEmail.Text,
                    MaLoaiNV = cboLoaiNV.SelectedValue.ToString()

                };


                // Chuyển đổi đối tượng thành JSON
                string jsonData = JsonConvert.SerializeObject(nhanVien);
                // Gọi API để thay đổi mật khẩu
                string url = $"{_url}suaThongTinNhanVien";
                bool result = await callAPI.PostAPI(url, jsonData);
                if(result)
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin nhân viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        #endregion


    }
}
