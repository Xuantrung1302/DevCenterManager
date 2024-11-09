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
    public partial class frmGiangVienEdit : Form
    {
        private bool isInsert;
        private DataTable _gv;
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";
        private string _url2 = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Teacher/";
        public frmGiangVienEdit(DataTable gv)
        {
            InitializeComponent();
            _gv = gv;
            isInsert = _gv == null;
        }

        /// <summary>
        /// Kiểm tra hợp lệ
        /// </summary>
        //public void ValidateLuu1()
        //{
        //    if (string.IsNullOrWhiteSpace(txtTenGV.Text))
        //        throw new ArgumentException("Họ và tên giảng viên không được trống");
        //    if (string.IsNullOrWhiteSpace(txtSDT.Text))
        //        throw new ArgumentException("Số điện thoại giảng viên không được trống");
        //    if (string.IsNullOrWhiteSpace(txtEmail.Text))
        //        throw new ArgumentException("Email giảng viên không được trống");
        //    if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
        //        throw new ArgumentException("Tên đăng nhập giảng viên không được trống");
        //    if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
        //        throw new ArgumentException("Mật khẩu giảng viên không được trống");
        //}

        private bool ValidateLuu()
        {
            if (txtTenGV.Text == "")
            {
                errorProvider1.SetError(txtTenGV, "Bạn chưa nhập họ và tên");
                txtTenGV.Focus();
                return false;
            }
            else if (txtSDT.Text == "")
            {
                errorProvider1.SetError(txtSDT, "Bạn chưa nhập số điện thoại");
                txtSDT.Focus();
                return false;
            }
            else if (txtEmail.Text == "")
            {
                errorProvider1.SetError(txtEmail, "Bạn chưa nhập email");
                txtEmail.Focus();
                return false;
            }
            else if (txtTenDangNhap.Text == "")
            {
                errorProvider1.SetError(txtTenDangNhap, "Bạn chưa nhập tên đăng nhập");
                txtTenDangNhap.Focus();
                return false;
            }
            else if (txtMatKhau.Text == "")
            {
                errorProvider1.SetError(txtMatKhau, "Bạn chưa nhập số mật khẩu");
                txtMatKhau.Focus();
                return false;
            }
            return true;
        }

        private void FillData(DataRow gv)
        {
            txtMaGV.Text = gv["MaGV"].ToString();
            txtTenGV.Text = gv["TenGV"].ToString();

            cboGioiTinh.Text = gv["GioiTinhGV"].ToString();
            txtSDT.Text = gv["SdtGV"].ToString();
            txtEmail.Text = gv["EmailGV"].ToString();

            txtTenDangNhap.Text = gv["TenDangNhap"].ToString();
            txtMatKhau.Text = gv["MatKhau"].ToString();

        }
        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
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
                errorProvider1.Clear();
                if (ValidateLuu())
                {
                    if (isInsert)
                    {
                        //Dieu kien khi them

                        GiangVien giangVien = new GiangVien()
                        {
                            MaGV = txtMaGV.Text,
                            TenGV = txtTenGV.Text,
                            GioiTinhGV = cboGioiTinh.SelectedItem.ToString(),
                            //NgaySinhGV = DateTime.Parse(dateNgaySinh.Value.ToString()).Date, // Chuyển đổi giá trị từ DateTimePicker
                            //DiaChi = txtDiaChi.Text,
                            SdtGV = txtSDT.Text,
                            EmailGV = txtEmail.Text,
                            //NgayTiepNhan = DateTime.Now.Date,
                            //MaLoaiHV = cboLoaiHV.SelectedValue.ToString(),
                            TenDangNhap = txtTenDangNhap.Text,
                            MatKhau = txtMatKhau.Text
                        };

                        // Chuyển đổi đối tượng thành JSON
                        string jsonData = JsonConvert.SerializeObject(giangVien);

                        string url = $"{_url2}themThongTinGiangVien";
                        bool result = await callAPI.PostAPI(url, jsonData);
                        if (result)
                        {
                            MessageBox.Show("Thêm thông tin giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thông tin giảng viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        try
                        {
                            //Dieu kien khi sua

                            GiangVien giangVien = new GiangVien()
                            {
                                MaGV = txtMaGV.Text,
                                TenGV = txtTenGV.Text,
                                GioiTinhGV = cboGioiTinh.SelectedItem.ToString(),
                                //NgaySinhGV = DateTime.Parse(dateNgaySinh.Value.ToString()).Date, // Chuyển đổi giá trị từ DateTimePicker
                                //DiaChi = txtDiaChi.Text,
                                SdtGV = txtSDT.Text,
                                EmailGV = txtEmail.Text,
                                //NgayTiepNhan = DateTime.Now.Date,
                                //MaLoaiHV = cboLoaiHV.SelectedValue.ToString(),
                                TenDangNhap = txtTenDangNhap.Text,
                                MatKhau = txtMatKhau.Text
                            };

                            // Chuyển đổi đối tượng thành JSON
                            string jsonData = JsonConvert.SerializeObject(giangVien);

                            string url = $"{_url2}suaThongTinGiangVien";
                            bool result = await callAPI.PostAPI(url, jsonData);
                            if (result)
                            {
                                MessageBox.Show("Cập nhật thông tin giảng viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thông tin giảng viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void frmGiangVienEdit_Load(object sender, EventArgs e)
        {
            string url = $"{_url}taoIdTuDong?ngay={DateTime.Now.Date.ToString("dd/MM/yyyy")}&ma=GV";
            string result = await callAPI.CallApiAsync(url);
            if (_gv == null)
            {
                txtMaGV.Text = result.Trim('"');
                txtTenDangNhap.ReadOnly = false;
                cboGioiTinh.SelectedIndex = 0;
            }
            if (_gv != null && _gv.Rows.Count > 0)
            {
                FillData(_gv.Rows[0]);
            }
        }
    }
}
