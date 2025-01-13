using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DevEduManager.Screens
{
    public partial class frmTrangMoDau : Form
    {
        private string _userName;
        public frmTrangMoDau(string userFullName)
        {
            InitializeComponent();
            _userName = userFullName;
        }
        CallAPI callAPI = new CallAPI();
        private string _url = $"{ConfigurationManager.AppSettings["HOST_API_URL"]}api/Service/";

        private async void frmTrangMoDau_Load(object sender, EventArgs e)
        {
            //string url = $"{_url}thongTinTrungTam"; 
            //DataTable result = await callAPI.GetAPI(url);
            //frmMain frm = new frmMain();

            //if (result.Rows.Count > 0)
            //{
            //    DataRow row = result.Rows[0];
            //    lblCenter.Text = string.Format("TRUNG TÂM LẬP TRÌNH {0}", row["TenTT"]).ToUpper();
            //    lblAddress.Text = string.Format("Địa chỉ: {0}", row["DiaChiTT"]);
            //    lblLienHe.Text = string.Format("Liên hệ: {0} - {1}", row["SdtTT"], row["EmailTT"]);
            //    lblWelcome.Text = string.Format("Xin chào, {0}", _userName);
            //}
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/");
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
