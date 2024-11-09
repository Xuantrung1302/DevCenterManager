using BusinessLogic;
using Enity.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace DevEduManager.Modals
{
    public partial class frmHocVienEdit : Form
    {
        private DataTable _hv;
        private bool isInsert = true;
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";
        private string _url2 = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Students/";
        public frmHocVienEdit(DataTable hv)
        {
            InitializeComponent();
            _hv = hv;
            isInsert = _hv == null;
        }
        private bool ValidateLuu()
        {
            if (txtHoTen.Text == "")
            {
                errorProvider1.SetError(txtHoTen, "Bạn chưa nhập họ và tên");
                txtHoTen.Focus();
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
            else if (txtDiaChi.Text == "")
            {
                errorProvider1.SetError(txtDiaChi, "Bạn chưa nhập địa chỉ");
                txtDiaChi.Focus();
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


        private async void frmHocVienEdit_Load(object sender, EventArgs e)
        {
            string url = $"{_url}taoIdTuDong?ngay={DateTime.Now.Date.ToString("dd/MM/yyyy")}&ma=HV";
            string result = await callAPI.CallApiAsync(url);
            if (_hv == null)
            {
                Common.LoadComboBoxLoaiHV(cboLoaiHV);
                txtMaHV.Text = result.Trim('"');
            }
            if (_hv != null && _hv.Rows.Count > 0)
            {
                Common.LoadComboBoxLoaiHV(cboLoaiHV);
                FillData(_hv.Rows[0]);
            }
        }

        private void FillData(DataRow hv)
        {
            txtMaHV.Text = hv["MaHV"].ToString();
            txtHoTen.Text = hv["TenHV"].ToString();

            if (hv["NgaySinh"] != DBNull.Value)
                dateNgaySinh.Value = Convert.ToDateTime(hv["NgaySinh"]);

            cboGioiTinh.Text = hv["GioiTinhHV"].ToString();
            txtDiaChi.Text = hv["DiaChi"].ToString();
            txtSDT.Text = hv["SdtHV"].ToString();
            txtEmail.Text = hv["EmailHV"].ToString();
            cboLoaiHV.SelectedValue = hv["MaLoaiHV"].ToString();


            if (hv["MaLoaiHV"].ToString() == "LHV01")
            {
                cboLoaiHV.Enabled = false;
                txtMatKhau.Enabled = false;
            }
            else
            {
                cboLoaiHV.Enabled = true;
                txtMatKhau.Enabled = false;
            }

            if (hv["TenDangNhap"] != DBNull.Value)
            {
                txtTenDangNhap.Text = hv["TenDangNhap"].ToString();
                txtMatKhau.Text = hv["MatKhau"].ToString();
            }
            else
            {
                txtTenDangNhap.Text = string.Empty;
                txtMatKhau.Text = string.Empty;
            }
        }

        private void cboLoaiHV_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboLoaiHV.SelectedValue.ToString() != "LHV00")
            {
                txtMatKhau.Enabled = true;
                txtTenDangNhap.Text = txtMaHV.Text;
                txtMatKhau.Text = txtMaHV.Text;
            }
            else
            {
                txtMatKhau.Enabled = false;
                txtTenDangNhap.Text = string.Empty;
                txtMatKhau.Text = string.Empty;
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLuu())
                {
                    if (isInsert)
                    {
                        //Dieu kien khi them

                        HocVien hocVien = new HocVien()
                        {
                            MaHV = txtMaHV.Text,
                            TenHV = txtHoTen.Text,
                            GioiTinhHV = cboGioiTinh.SelectedItem.ToString(),
                            NgaySinh = DateTime.Parse(dateNgaySinh.Value.ToString()).Date, // Chuyển đổi giá trị từ DateTimePicker
                            DiaChi = txtDiaChi.Text,
                            SdtHV = txtSDT.Text,
                            EmailHV = txtEmail.Text,
                            NgayTiepNhan = DateTime.Now.Date,
                            MaLoaiHV = cboLoaiHV.SelectedValue.ToString(),
                            TenDangNhap = txtTenDangNhap.Text,
                            MatKhau = txtMatKhau.Text
                        };

                        // Chuyển đổi đối tượng thành JSON
                        string jsonData = JsonConvert.SerializeObject(hocVien);

                        string url = $"{_url2}themThongTinHocVien";
                        bool result = await callAPI.PostAPI(url, jsonData);
                        if (result)
                        {
                            MessageBox.Show("Thêm thông tin học viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thông tin học viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        try
                        {
                            //Dieu kien khi them

                            HocVien hocVien = new HocVien()
                            {
                                MaHV = txtMaHV.Text,
                                TenHV = txtHoTen.Text,
                                GioiTinhHV = cboGioiTinh.SelectedItem.ToString(),
                                NgaySinh = DateTime.Parse(dateNgaySinh.Value.ToString()).Date, // Chuyển đổi giá trị từ DateTimePicker
                                DiaChi = txtDiaChi.Text,
                                SdtHV = txtSDT.Text,
                                EmailHV = txtEmail.Text,
                                MaLoaiHV = cboLoaiHV.SelectedValue.ToString(),
                                TenDangNhap = txtTenDangNhap.Text,
                                MatKhau = txtMatKhau.Text
                            };

                            // Chuyển đổi đối tượng thành JSON
                            string jsonData = JsonConvert.SerializeObject(hocVien);

                            string url = $"{_url2}suaThongTinHocVien";
                            bool result = await callAPI.PostAPI(url, jsonData);
                            if (result)
                            {
                                MessageBox.Show("Cập nhật thông tin học viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
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
