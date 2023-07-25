using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyNhaThuocTay.Class;

namespace QuanLyNhaThuocTay
{
    public partial class frmTimHDBan : Form
    {
        DataTable tblHDB;
        public frmTimHDBan()
        {
            InitializeComponent();
        }

        private void frmTimHDBan_Load(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaHoaDon.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaHoaDon.Text == "") &&
               (txtMaNhanVien.Text == "") && (txtMaKhachHang.Text == "") &&
               (txtTongTien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblHDBan WHERE 1=1";
            if (txtMaHoaDon.Text != "")
                sql = sql + " AND MaHoaDon Like N'%" + txtMaHoaDon.Text + "%'";
            if (txtMaNhanVien.Text != "")
                sql = sql + " AND MaNhanVien Like N'%" + txtMaNhanVien.Text + "%'";
            if (txtMaKhachHang.Text != "")
                sql = sql + " AND MaKhach Like N'%" + txtMaKhachHang.Text + "%'";
            if (txtTongTien.Text != "")
                sql = sql + " AND TongTien <=" + txtTongTien.Text;
            tblHDB = Functions.GetDataToDataTable(sql);
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Có " + tblHDB.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvTKHoaDon.DataSource = tblHDB;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            dgvTKHoaDon.Columns[0].HeaderText = "Mã HĐB";
            dgvTKHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dgvTKHoaDon.Columns[2].HeaderText = "Ngày bán";
            dgvTKHoaDon.Columns[3].HeaderText = "Mã khách";
            dgvTKHoaDon.Columns[4].HeaderText = "Tổng tiền";
            dgvTKHoaDon.Columns[0].Width = 150;
            dgvTKHoaDon.Columns[1].Width = 100;
            dgvTKHoaDon.Columns[2].Width = 80;
            dgvTKHoaDon.Columns[3].Width = 80;
            dgvTKHoaDon.Columns[4].Width = 80;
            dgvTKHoaDon.AllowUserToAddRows = false;
            dgvTKHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimLai_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

        private void txtTongTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            ResetValues();
            dgvTKHoaDon.DataSource = null;
        }

      
    } }
