using BusinessLogic;
using Enity.Models;
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

namespace DevEduManager.Screens
{
    public partial class frmTiepNhanHocVien : Form
    {
        public frmTiepNhanHocVien()
        {
            InitializeComponent();
        }
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Students/";


        #region Events
        #region Button Events
        private void btnThem_Click(object sender, EventArgs e)
        {

        }
        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnLuuThongTin_Click(object sender, EventArgs e)
        {

        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            LockPanelControl();
            gridDSHV_Click(sender, e);
        }
        #endregion
        private void frmTiepNhanHocVien_Load(object sender, EventArgs e)
        {
            LoadDataToGridView();
        }

        private async void LoadDataToGridView()
        {
            string url = $"{_url}thongTinHocVien";
            DataTable result = await callAPI.GetAPI(url);
            // Giả sử bạn có một DataGridView tên là dataGridView1 và Panel tên là panel1
            gridDSHV.Dock = DockStyle.Fill;

            if (result.Rows.Count > 0)
            {
                gridDSHV.DataSource = result;
            }
        }
        public void LockPanelControl()
        {
            txtHoTen.Enabled = false;
            dateNgaySinh.Enabled = false;
            cboGioiTinh.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            txtEmail.Enabled = false;
            cboLoaiHV.Enabled = false;
            txtMatKhau.Enabled = false;
            btnLuuThongTin.Enabled = false;
            btnHuyBo.Enabled = false;
        }
        private void gridDSHV_Click(object sender, EventArgs e)
        {
            //LockPanelControl();
            //hocVien = HocVien.Select(gridDSHV.SelectedRows[0].Cells["clmMaHV"].Value.ToString());
            //LoadPanelControl(hocVien);
        }

        private void gridDSHV_DoubleClick(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
