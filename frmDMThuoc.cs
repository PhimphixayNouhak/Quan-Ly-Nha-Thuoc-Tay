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
    public partial class frmDMThuoc : Form
    {
        DataTable tblT;
        public frmDMThuoc()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void frmDMThuoc_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * from tblChatLieu";
            txtMaThuoc.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboMaChatLieu, "MaChatLieu", "TenChatLieu");
            cboMaChatLieu.SelectedIndex = -1;
            ResetValues();
            // TODO: This line of code loads data into the 'quanLyNhaThuocTayDataSet1.tblThuoc' table. You can move, or remove it, as needed.
            this.tblThuocTableAdapter.Fill(this.quanLyNhaThuocTayDataSet1.tblThuoc);

        }

        private void ResetValues()
        {
            txtMaThuoc.Text = "";
            txtTenThuoc.Text = "";
            cboMaChatLieu.Text = "";
            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";
            txtDonGiaBan.Text = "0";
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtAnh.Text = "";
            picAnh.Image = null;
            txtGhiChu.Text = "";
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from tblThuoc";
            tblT = Functions.GetDataToDataTable(sql);
            dgvThuoc.DataSource = tblT;
            dgvThuoc.Columns[0].HeaderText = "Mã thuốc";
            dgvThuoc.Columns[1].HeaderText = "Tên thuốc";
            dgvThuoc.Columns[2].HeaderText = "Chất liệu";
            dgvThuoc.Columns[3].HeaderText = "Số lượng";
            dgvThuoc.Columns[4].HeaderText = "Đơn giá nhập";
            dgvThuoc.Columns[5].HeaderText = "Đơn giá bán";
            dgvThuoc.Columns[6].HeaderText = "Ảnh";
            dgvThuoc.Columns[7].HeaderText = "Ghi chú";
            dgvThuoc.Columns[0].Width = 80;
            dgvThuoc.Columns[1].Width = 140;
            dgvThuoc.Columns[2].Width = 80;
            dgvThuoc.Columns[3].Width = 80;
            dgvThuoc.Columns[4].Width = 100;
            dgvThuoc.Columns[5].Width = 100;
            dgvThuoc.Columns[6].Width = 200;
            dgvThuoc.Columns[7].Width = 300;
            dgvThuoc.AllowUserToAddRows = false;
            dgvThuoc.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaThuoc.Enabled = true;
            txtMaThuoc.Focus();
            txtSoLuong.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            if (txtTenThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenThuoc.Focus();
                return;
            }
            if (cboMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaChatLieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOpen.Focus();
                return;
            }
            sql = "SELECT MaHang FROM tblHang WHERE MaHang=N'" + txtMaThuoc.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã tồn tại, bạn phải chọn mã hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            sql = "INSERT INTO tblHang(MaHang,TenHang,MaChatLieu,SoLuong,DonGiaNhap, DonGiaBan,Anh,Ghichu) VALUES(N'"
                + txtMaThuoc.Text.Trim() + "',N'" + txtTenThuoc.Text.Trim() +
                "',N'" + cboMaChatLieu.SelectedValue.ToString() +
                "'," + txtSoLuong.Text.Trim() + "," + txtDonGiaNhap.Text +
                "," + txtDonGiaBan.Text + ",'" + txtAnh.Text + "',N'" + txtGhiChu.Text.Trim() + "')";

            Functions.RunSQL(sql);
            LoadDataGridView();
            //ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaThuoc.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaThuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            if (txtTenThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenThuoc.Focus();
                return;
            }
            if (cboMaChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaChatLieu.Focus();
                return;
            }
            if (txtAnh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải ảnh minh hoạ cho hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAnh.Focus();
                return;
            }
            sql = "UPDATE tblHang SET TenThuoc=N'" + txtTenThuoc.Text.Trim().ToString() +
                "',MaChatLieu=N'" + cboMaChatLieu.SelectedValue.ToString() +
                "',SoLuong=" + txtSoLuong.Text +
                ",Anh='" + txtAnh.Text + "',Ghichu=N'" + txtGhiChu.Text + "' WHERE MaHang=N'" + txtMaThuoc.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblT.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaThuoc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblHang WHERE MaThuoc=N'" + txtMaThuoc.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnThem.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaThuoc.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaThuoc.Text == "") && (txtTenThuoc.Text == "") && (cboMaChatLieu.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * from tblHang WHERE 1=1";
            if (txtMaThuoc.Text != "")
                sql += " AND MaThuoc LIKE N'%" + txtMaThuoc.Text + "%'";
            if (txtTenThuoc.Text != "")
                sql += " AND TenThuoc LIKE N'%" + txtTenThuoc.Text + "%'";
            if (cboMaChatLieu.Text != "")
                sql += " AND MaChatLieu LIKE N'%" + cboMaChatLieu.SelectedValue + "%'";
            tblT = Functions.GetDataToDataTable(sql);
            if (tblT.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thoả mãn điều kiện tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else MessageBox.Show("Có " + tblT.Rows.Count + "  bản ghi thoả mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dgvThuoc.DataSource = tblT;
            ResetValues();
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaThuoc,TenThuoc,MaChatLieu,SoLuong,DonGiaNhap,DonGiaBan,Anh,GhiChu FROM tblThuoc";
            tblT = Functions.GetDataToDataTable(sql);
            dgvThuoc.DataSource = tblT;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string MaChatLieu;
            string sql;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaThuoc.Focus();
                return;
            }
            if (tblT.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaThuoc.Text = dgvThuoc.CurrentRow.Cells["MaThuoc"].Value.ToString();
            txtTenThuoc.Text = dgvThuoc.CurrentRow.Cells["TenThuoc"].Value.ToString();
            MaChatLieu = dgvThuoc.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            sql = "SELECT TenChatLieu FROM tblChatLieu WHERE MaChatLieu=N'" + MaChatLieu + "'";
            cboMaChatLieu.Text = Functions.GetFieldValues(sql);
            txtSoLuong.Text = dgvThuoc.CurrentRow.Cells["SoLuong"].Value.ToString();
            txtDonGiaNhap.Text = dgvThuoc.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
            txtDonGiaBan.Text = dgvThuoc.CurrentRow.Cells["DonGiaBan"].Value.ToString();
            sql = "SELECT Anh FROM tblT WHERE MaThuoc=N'" + txtMaThuoc.Text + "'";
            txtAnh.Text = Functions.GetFieldValues(sql);
            picAnh.Image = Image.FromFile(txtAnh.Text);
            sql = "SELECT Ghichu FROM tblT WHERE MaThuoc = N'" + txtMaThuoc.Text + "'";
            txtGhiChu.Text = Functions.GetFieldValues(sql);
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }
    }
}
