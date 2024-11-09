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
    public partial class frmNhanVienEdit : Form
    {
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";
        private string _url2 = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Employee/";
        DataTable _nv;
        bool isInsert = true;
        public frmNhanVienEdit(DataTable nv)
        {
            InitializeComponent();
            _nv = nv;
            isInsert = _nv == null;
        }
        private bool ValidateLuu()
        {
            if (txtTenNV.Text == "")
            {
                errorProvider1.SetError(txtTenNV, "Bạn chưa nhập họ và tên");
                txtTenNV.Focus();
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
                errorProvider1.SetError(txtMatKhau, "Bạn chưa nhập mật khẩu");
                txtMatKhau.Focus();
                return false;
            }
            return true;
        }

        private async void frmNhanVienEdit_Load(object sender, EventArgs e)
        {
            string url = $"{_url}taoIdTuDong?ngay={DateTime.Now.Date.ToString("dd/MM/yyyy")}&ma=NV";
            string result = await callAPI.CallApiAsync(url);
            if (_nv == null)
            {
                Common.LoadComboBoxLoaiNV(cboLoaiNV);
                txtMaNV.Text = result.Trim('"');
                txtTenDangNhap.ReadOnly = false;
            }
            if (_nv != null && _nv.Rows.Count > 0)
            {
                Common.LoadComboBoxLoaiNV(cboLoaiNV);
                FillData(_nv.Rows[0]);
                txtTenDangNhap.ReadOnly = true;
            }
        }
        private void FillData(DataRow hv)
        {
            txtMaNV.Text = hv["MaNV"].ToString();
            txtTenNV.Text = hv["TenNV"].ToString();

            //if (hv["NgaySinh"] != DBNull.Value)
            //    dateNgaySinh.Value = Convert.ToDateTime(hv["NgaySinh"]);

            //cboGioiTinh.Text = hv["GioiTinhHV"].ToString();
            //txtDiaChi.Text = hv["DiaChi"].ToString();
            txtSDT.Text = hv["SdtNV"].ToString();
            txtEmail.Text = hv["EmailNV"].ToString();
            cboLoaiNV.SelectedValue = hv["MaLoaiNV"].ToString();


            //if (hv["MaLoaiHV"].ToString() == "LHV01")
            //{
            //    cboLoaiHV.Enabled = false;
            //    txtMatKhau.Enabled = false;
            //}
            //else
            //{
            //    cboLoaiHV.Enabled = true;
            //    txtMatKhau.Enabled = false;
            //}

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

        private async void btnLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateLuu())
                {
                    if (isInsert)
                    {
                        //Dieu kien khi them

                        NhanVien nhanVien = new NhanVien()
                        {
                            MaNV = txtMaNV.Text,
                            TenNV = txtTenNV.Text,
                            //GioiTinhHV = cboGioiTinh.SelectedItem.ToString(),
                            //NgaySinh = DateTime.Parse(dateNgaySinh.Value.ToString()).Date, // Chuyển đổi giá trị từ DateTimePicker
                            //DiaChi = txtDiaChi.Text,
                            SdtNV = txtSDT.Text,
                            EmailNV = txtEmail.Text,
                            //NgayTiepNhan = DateTime.Now.Date,
                            MaLoaiNV = cboLoaiNV.SelectedValue.ToString(),
                            TenDangNhap = txtTenDangNhap.Text,
                            MatKhau = txtMatKhau.Text
                        };

                        // Chuyển đổi đối tượng thành JSON
                        string jsonData = JsonConvert.SerializeObject(nhanVien);

                        string url = $"{_url2}themThongTinNhanVien";
                        bool result = await callAPI.PostAPI(url, jsonData);
                        if (result)
                        {
                            MessageBox.Show("Thêm thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thông tin nhân viên không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        try
                        {
                            //Dieu kien khi them

                            NhanVien nhanVien = new NhanVien()
                            {
                                MaNV = txtMaNV.Text,
                                TenNV = txtTenNV.Text,
                                //GioiTinhHV = cboGioiTinh.SelectedItem.ToString(),
                                //NgaySinh = DateTime.Parse(dateNgaySinh.Value.ToString()).Date, // Chuyển đổi giá trị từ DateTimePicker
                                //DiaChi = txtDiaChi.Text,
                                SdtNV = txtSDT.Text,
                                EmailNV = txtEmail.Text,
                                //NgayTiepNhan = DateTime.Now.Date,
                                MaLoaiNV = cboLoaiNV.SelectedValue.ToString(),
                                TenDangNhap = txtTenDangNhap.Text,
                                MatKhau = txtMatKhau.Text
                            };

                            // Chuyển đổi đối tượng thành JSON
                            string jsonData = JsonConvert.SerializeObject(nhanVien);

                            string url = $"{_url2}suaThongTinNhanVien";
                            bool result = await callAPI.PostAPI(url, jsonData);
                            if (result)
                            {
                                MessageBox.Show("Cập nhật thông tin nhân viên thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
