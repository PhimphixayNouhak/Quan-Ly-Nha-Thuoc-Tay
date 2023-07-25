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
    public partial class frmHDBan : Form
    {
        DataTable tblHDB;
        public frmHDBan()
        {
            InitializeComponent();
        }

        private void frmHDBan_Load(object sender, EventArgs e)
        {
            txtMaHoaDon.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            // TODO: This line of code loads data into the 'quanLyNhaThuocTayDataSet6.tblHDBan' table. You can move, or remove it, as needed.
            this.tblHDBanTableAdapter.Fill(this.quanLyNhaThuocTayDataSet6.tblHDBan);

        }
        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaHDBan,MaNhanVien,NgayBan,MaKhach,TongTien FROm tblHDBan";
            tblHDB = Functions.GetDataToDataTable(sql); //lấy dữ liệu
            dgvHoaDon.DataSource = tblHDB;
            dgvHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
            dgvHoaDon.Columns[1].HeaderText = "Mã nhân viên";
            dgvHoaDon.Columns[2].HeaderText = "Ngày bán";
            dgvHoaDon.Columns[3].HeaderText = "Mã khách hàng";
            dgvHoaDon.Columns[4].HeaderText = "Tống tiền";
            dgvHoaDon.Columns[0].Width = 100;
            dgvHoaDon.Columns[1].Width = 150;
            dgvHoaDon.Columns[2].Width = 100;
            dgvHoaDon.Columns[3].Width = 150;
            dgvHoaDon.Columns[4].Width = 100;
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvHoaDon_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
               
            }
            if (tblHDB.Rows.Count == 0)
            {
               
            }
            txtMaHoaDon.Text = dgvHoaDon.CurrentRow.Cells["MaHoaDon"].Value.ToString();
            txtMaNhanVien.Text = dgvHoaDon.CurrentRow.Cells["MaNhanVien"].Value.ToString();
            mskNgayBan.Text = dgvHoaDon.CurrentRow.Cells["NgayBan"].Value.ToString();
            txtMaKhachHang.Text = dgvHoaDon.CurrentRow.Cells["MaKachHang"].Value.ToString();
            textTongTien.Text = dgvHoaDon.CurrentRow.Cells["TongTien"].Value.ToString();
            btnXoa.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHoaDon.Enabled = true;
            txtMaHoaDon.Focus();
        }

        private void ResetValues()
        {
            txtMaHoaDon.Text = "";
            txtMaKhachHang.Text = "";
            mskNgayBan.Text = "";
            txtMaNhanVien.Text = "";
            textTongTien.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaHoaDon.Text.Trim().Length == 0)
            {
             
            }
            if (txtMaNhanVien.Text.Trim().Length == 0)
            {
               
            }
            if (textTongTien.Text.Trim().Length == 0)
            {
              
            }
            if (txtMaKhachHang.Text.Trim().Length == 0)
            {
               
            }
            if (!Functions.IsDate(mskNgayBan.Text))
            {
           
                {
                    MessageBox.Show("Mã hóa đơn này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaHoaDon.Focus();
                    txtMaHoaDon.Text = "";
                    return;
                }
#pragma warning disable CS0162 // Unreachable code detected
                sql = "INSERT INTO tblHDBan(MaHoaDon,MaNhanVien,NgayBan,MaKhachHang,TongTien) VALUES (N'" + txtMaHoaDon.Text.Trim() + "',N'" + txtMaNhanVien.Text.Trim() + "'N'" + txtMaKhachHang.Text.Trim() + "',N'" + textTongTien.Text.Trim() + "',N'" + gt + "',N'" + Functions.ConvertDateTime(mskNgayBan.Text) + "')";
#pragma warning restore CS0162 // Unreachable code detected
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
                btnXoa.Enabled = true;
                btnThem.Enabled = true;
                btnBoQua.Enabled = false;
                btnLuu.Enabled = false;
                txtMaHoaDon.Enabled = false;
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaHoaDon.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblHDBan WHERE MaHoaDon=N'" + txtMaHoaDon.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            txtMaHoaDon.Enabled = false;
        }

        private void txtMaHoaDon_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
