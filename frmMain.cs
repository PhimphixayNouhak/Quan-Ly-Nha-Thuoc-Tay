using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyNhaThuocTay.Class;

namespace QuanLyNhaThuocTay
{
    public partial class frmMain : Form
    {
        public object Class { get; private set; }

        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuVaiNet_Click(object sender, EventArgs e)
        {
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Functions.Disconnect();
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Functions.Connect();
        }

        private void mnuChatLieu_Click(object sender, EventArgs e)
        {
            frmDMChatLieu frmChatlieu = new frmDMChatLieu();
            frmChatlieu.ShowDialog();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            frmDMNhanVien frmNhanVien = new frmDMNhanVien();
            frmNhanVien.MdiParent = this;
            frmNhanVien.Show();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            frmDMKhachHang frmKhachHang = new frmDMKhachHang();
            frmKhachHang.MdiParent = this;
            frmKhachHang.Show();
        }

        private void mnuThuocHoa_Click(object sender, EventArgs e)
        {
            frmDMThuoc frmThuocHoa = new frmDMThuoc();
            frmThuocHoa.MdiParent = this;
            frmThuocHoa.Show();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            frmHDBan frm = new frmHDBan();
            frm.MdiParent = this;
            frm.Show();
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            frmTimHDBan frmTimHD = new frmTimHDBan();
            frmTimHD.MdiParent = this;
            frmTimHD.Show();
        }
    }
}
