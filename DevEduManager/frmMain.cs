using BusinessLogic;
using DevEduManager.Modals;
using DevEduManager.Screens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DevEduManager
{
    public partial class frmMain : Form
    {
        private DataTable userName;
        private DataTable receivedData;
        //private DataTable _user = new DataTable();
        public frmMain()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");
            //this._user = user;
        }

        #region Ribbon bar

        #region Ribbon Tab

        /// <summary>
        /// Phục hồi màu sắc tiêu đề tab
        /// </summary>
        private void ResetColorTabTitle()
        {
            btnNhanVienTitle.BackColor = Color.White;
            btnGiangVienTitle.BackColor = Color.White;
            btnHocVienTitle.BackColor = Color.White;
            btnQuanTriTitle.BackColor = Color.White;
            btnTroGiupTitle.BackColor = Color.White;
        }

        private void btnNhanVienTitle_Click(object sender, EventArgs e)
        {
            tabRibbon.SelectedTab = tabRibbon.TabPages["tabNhanVien"];

            ResetColorTabTitle();
            ((Button)sender).BackColor = Color.FromArgb(233, 233, 233);
        }

        private void btnGiangVienTitle_Click_1(object sender, EventArgs e)
        {
            tabRibbon.SelectedTab = tabRibbon.TabPages["tabGiangVien"];

            ResetColorTabTitle();
            ((Button)sender).BackColor = Color.FromArgb(233, 233, 233);
        }

        private void btnHocVienTitle_Click_1(object sender, EventArgs e)
        {
            tabRibbon.SelectedTab = tabRibbon.TabPages["tabHocVien"];

            ResetColorTabTitle();
            ((Button)sender).BackColor = Color.FromArgb(233, 233, 233);
        }

        private void btnQuanLyLopHoc_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            frmQuanLyLopHoc frm = new frmQuanLyLopHoc()
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }
        private void btnQuanLyKhoaHoc_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            frmQuanLyKhoaHoc frm = new frmQuanLyKhoaHoc()
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }

        private void btnQuanTriTitle_Click_1(object sender, EventArgs e)
        {
            tabRibbon.SelectedTab = tabRibbon.TabPages["tabQuanTri"];

            ResetColorTabTitle();
            ((Button)sender).BackColor = Color.FromArgb(233, 233, 233);
        }
        
        private void btnTroGiupTitle_Click_1(object sender, EventArgs e)
        {
            tabRibbon.SelectedTab = tabRibbon.TabPages["tabTroGiup"];

            ResetColorTabTitle();
            ((Button)sender).BackColor = Color.FromArgb(233, 233, 233);
        }

        #endregion

        #region Ribbon Button
        private void btnThongTinTrungTam_Click_1(object sender, EventArgs e)
        {
            frmThongTinTrungTam frm = new frmThongTinTrungTam();
            frm.ShowDialog();
        }

        private void btnTrangMoDau_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            // Kiểm tra cột nào không null và lấy giá trị của cột đó
            string tenNguoiDung = string.Empty;

            if (receivedData.Rows[0]["TenNV"] != DBNull.Value)
            {
                tenNguoiDung = receivedData.Rows[0]["TenNV"].ToString();
            }
            else if (receivedData.Rows[0]["TenHV"] != DBNull.Value)
            {
                tenNguoiDung = receivedData.Rows[0]["TenHV"].ToString();
            }
            else if (receivedData.Rows[0]["TenGV"] != DBNull.Value)
            {
                tenNguoiDung = receivedData.Rows[0]["TenGV"].ToString();
            }

            // Truyền giá trị đã xác định sang form frmTrangMoDau
            frmTrangMoDau frm = new frmTrangMoDau(tenNguoiDung)
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }

        private void btnThayDoiThongTinNV_Click(object sender, EventArgs e)
        {
            string ma = receivedData.Rows[0]["MaNV"].ToString();
            frmThayDoiThongTinNV frm = new frmThayDoiThongTinNV(ma);
            frm.ShowDialog();
        }
        private void btnHVThayDoiThongTin_Click(object sender, EventArgs e)
        {
            string ma = receivedData.Rows[0]["MaHV"].ToString();
            frmThayDoiThongTinHV frm = new frmThayDoiThongTinHV(ma);
            frm.ShowDialog();
        }
        private void btnGVThayDoiThongTin_Click(object sender, EventArgs e)
        {
            string ma = receivedData.Rows[0]["MaGV"].ToString();
            frmThayDoiThongTinGV frm = new frmThayDoiThongTinGV(ma);
            frm.ShowDialog();
        }
        private void btnTiepNhanHocVien_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            frmTiepNhanHocVien frm = new frmTiepNhanHocVien()
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }
        private void btnQuanLyGiangVien_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            frmQuanLyGiangVien frm = new frmQuanLyGiangVien()
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }
        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            frmQuanLyNhanVien frm = new frmQuanLyNhanVien()
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }
        private void btnQuanLyHocVien_Click(object sender, EventArgs e)
        {
            pnlWorkspace.Controls.Clear();

            frmQuanLyHocVien frm = new frmQuanLyHocVien()
            {
                Dock = DockStyle.Fill,
                TopLevel = false
            };

            pnlWorkspace.Controls.Add(frm);
            frm.Show();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap();
        }



        private void btnKetNoiCSDL_Click(object sender, EventArgs e)
        {
            frmKetNoiCSDL frm = new frmKetNoiCSDL();
            frm.ShowDialog();
        }

        private void btnNVDoiMatKhau_Click(object sender, EventArgs e)
        {
            MoFormDoiMatKhau(receivedData);
        }

        private void btnHVDoiMatKhau_Click(object sender, EventArgs e)
        {
            MoFormDoiMatKhau(receivedData);
        }

        private void btnGVDoiMatKhau_Click(object sender, EventArgs e)
        {
            MoFormDoiMatKhau(receivedData);
        }

        private void MoFormDoiMatKhau(DataTable receivedData)
        {
            string tenNguoiDung = string.Empty;
            string tenDangNhap = receivedData.Rows[0]["TenDangNhap"].ToString();
            string matKhau = receivedData.Rows[0]["MatKhau"].ToString();

            if (receivedData.Rows[0]["TenNV"] != DBNull.Value)
            {
                tenNguoiDung = receivedData.Rows[0]["TenNV"].ToString();
            }
            else if (receivedData.Rows[0]["TenHV"] != DBNull.Value)
            {
                tenNguoiDung = receivedData.Rows[0]["TenHV"].ToString();
            }
            else if (receivedData.Rows[0]["TenGV"] != DBNull.Value)
            {
                tenNguoiDung = receivedData.Rows[0]["TenGV"].ToString();
            }

            frmDoiMatKhau frm = new frmDoiMatKhau(tenNguoiDung, tenDangNhap, matKhau);
            frm.ShowDialog();
        }

        #endregion

        /// <summary>
        /// Phục hồi trạng thái enable của Ribbon control
        /// </summary>
        public void ResetRibbonControlStatus()
        {
            btnQuanTriTitle.Visible = true;
            btnNhanVienTitle.Visible = true;
            btnHocVienTitle.Visible = true;
            btnGiangVienTitle.Visible = true;
            btnTroGiup.Visible = true;

            btnTiepNhanHocVien.Enabled = true;
            btnLapPhieuGhiDanh.Enabled = true;
            btnBaoCaoHocVienTheoThang.Enabled = true;
            btnThongKeNoHocVien.Enabled = true;
            btnThongKeDiemTheoLop.Enabled = true;
            btnQuanLyDiem.Enabled = true;
            btnXepLop.Enabled = true;
            btnQuanLyHocVien.Enabled = true;
            btnQuanLyNhanVien.Enabled = true;
            btnQuanLyGiangVien.Enabled = true;
            btnQuanLyLopHoc.Enabled = true;
            btnQuanLyKhoaHoc.Enabled = true;
            btnQuanLyHocPhi.Enabled = true;
            btnQuanLyTaiKhoan.Enabled = true;
            btnThayDoiQuyDinh.Enabled = true;
            btnKetNoiCSDL.Enabled = true;
            btnQuanLyTaiKhoan.Enabled = true;
            btnThongTinTrungTam.Enabled = true;
        }

        #endregion

        #region Đăng nhập và khởi động

        /// <summary>
        /// Đăng nhập vào phần mềm
        /// </summary>
        public void DangNhap()
        {
            this.Hide();
            try
            {
                frmDangNhap frm = new frmDangNhap();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    receivedData = frm.userData;

                    LoadGiaoDien(receivedData);

                    this.Show();

                    timer.Start();
                }
            }
            catch (Exception ex)
            {
               Reconnect();
            }
        }

        /// <summary>
        /// Kết nối lại cơ sở dữ liệu
        /// </summary>
        private void Reconnect()
        {
            frmKetNoiCSDL frm = new frmKetNoiCSDL();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (DialogResult.Yes == MessageBox.Show("Bạn cần khởi động lại chương trình để thay đổi có hiệu lực." + Environment.NewLine + "Khởi động ngay?", "Khởi động lại", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    Application.Restart();
                else
                    Application.Exit();
            }
            else
            {
                MessageBox.Show("Bạn không thể sử dụng chương trình nếu kết nối cơ sở dữ liệu chưa được thiết lập" + Environment.NewLine + "Hãy chạy lại chương trình vào lần tới để thiết lập lại kết nối cơ sở dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }


        /// <summary>
        /// Phân quyền người dùng dựa trên loại nhân viên và loại học viên
        /// </summary>
        /// <param name="userType">Mã loại nhân viên</param>
        /// <param name="userType2">Mã loại học viên</param>
        public void PhanQuyen(string maLoaiNV, string maLoaiHV)
        {
            if (!string.IsNullOrEmpty(maLoaiNV))
            {
                switch (maLoaiNV)
                {
                    case "LNV01": // Nhân viên ghi danh
                        btnGiangVienTitle.Visible = false;
                        btnHocVienTitle.Visible = false;
                        btnQuanTriTitle.Visible = false;
                        btnThongKeNoHocVien.Enabled = false;
                        btnThongKeDiemTheoLop.Enabled = false;
                        btnQuanLyDiem.Enabled = false;
                        btnXepLop.Enabled = false;
                        btnNhanVienTitle_Click(btnNhanVienTitle, null);
                        break;
                    case "LNV02": // Nhân viên học vụ
                        btnGiangVienTitle.Visible = false;
                        btnHocVienTitle.Visible = false;
                        btnTiepNhanHocVien.Enabled = false;
                        btnLapPhieuGhiDanh.Enabled = false;
                        btnBaoCaoHocVienTheoThang.Enabled = false;
                        btnThongKeNoHocVien.Enabled = false;
                        btnQuanLyNhanVien.Enabled = false;
                        btnQuanLyHocPhi.Enabled = false;
                        btnQuanLyTaiKhoan.Enabled = false;
                        btnThayDoiQuyDinh.Enabled = false;
                        btnKetNoiCSDL.Enabled = false;
                        btnThongTinTrungTam.Enabled = false;
                        btnNhanVienTitle_Click(btnNhanVienTitle, null);
                        break;
                    case "LNV03": // Nhân viên kế toán
                        btnGiangVienTitle.Visible = false;
                        btnHocVienTitle.Visible = false;
                        btnTiepNhanHocVien.Enabled = false;
                        btnLapPhieuGhiDanh.Enabled = false;
                        btnBaoCaoHocVienTheoThang.Enabled = false;
                        btnThongKeDiemTheoLop.Enabled = false;
                        btnQuanLyDiem.Enabled = false;
                        btnXepLop.Enabled = false;
                        btnQuanLyHocVien.Enabled = false;
                        btnQuanLyNhanVien.Enabled = false;
                        btnQuanLyGiangVien.Enabled = false;
                        btnQuanLyLopHoc.Enabled = false;
                        btnQuanLyKhoaHoc.Enabled = false;
                        btnQuanLyTaiKhoan.Enabled = false;
                        btnThayDoiQuyDinh.Enabled = false;
                        btnKetNoiCSDL.Enabled = false;
                        btnThongTinTrungTam.Enabled = false;
                        btnNhanVienTitle_Click(btnNhanVienTitle, null);
                        break;
                    default:
                        btnHocVienTitle.Visible = false;
                        btnGiangVienTitle.Visible = false;
                        btnQuanTriTitle_Click_1(btnQuanTriTitle, null);
                        break;
                }
            }
            else if (!string.IsNullOrEmpty(maLoaiHV))
            {
                // Nếu là học viên
                btnNhanVienTitle.Visible = false;
                btnQuanTriTitle.Visible = false;
                btnGiangVienTitle.Visible = false;
                btnQuanTriTitle_Click_1(this.btnHocVienTitle, null);
            }
            else
            {
                // Nếu là giảng viên hoặc khác
                btnNhanVienTitle.Visible = false;
                btnQuanTriTitle.Visible = false;
                btnHocVienTitle.Visible = false;
                btnGiangVienTitle_Click_1(this.btnGiangVienTitle, null);
            }
        }


        /// <summary>
        /// Nạp giao diện phần mềm
        /// </summary>
        public void LoadGiaoDien(DataTable userName)
        {
            lblUserName.Text = "Xin chào, " + userName.Rows[0]["TenDangNhap"].ToString();
            ResetRibbonControlStatus();
            string maLoaiNV = userName.Rows[0]["MaLoaiNV"].ToString();
            string maLoaiHV = userName.Rows[0]["MaLoaiHV"].ToString();
            //lblUserName.Text = GlobalSettings.UserName;

            PhanQuyen(maLoaiNV, maLoaiHV);
            pnlWorkspace.Controls.Clear();

            //if (GlobalSettings.UserType == UserType.NhanVien)
            //    GlobalPages.LoadEssentialPages();

            //GlobalSettings.LoadCenterInformation();
            //GlobalSettings.LoadQuyDinh();

            btnTrangMoDau_Click(null, null);
        }


        #endregion

        private void frmMain_Load(object sender, EventArgs e)
        {
            DangNhap();
        }


    }
}
