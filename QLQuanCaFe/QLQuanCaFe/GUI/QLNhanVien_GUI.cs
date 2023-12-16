using DAL_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaFe.GUI
{
    public partial class QLNhanVien_GUI : Form
    {
        NhanVienDAL_BLL nv = new NhanVienDAL_BLL();
        public QLNhanVien_GUI()
        {
            InitializeComponent();
            List<NhanVien> nvs = nv.getKhachHang();
            dgvNhanVien.DataSource = nvs;
            txtTimkiem.Text = "Tìm kiếm theo tên...";
            txtTimkiem.ForeColor = System.Drawing.Color.Gray;
        }

        private void QLNhanVien_GUI_Load(object sender, EventArgs e)
        {
            dgvNhanVien.Columns[0].HeaderText = "Mã NV";
            dgvNhanVien.Columns[1].HeaderText = "Tên NV";
            dgvNhanVien.Columns[2].HeaderText = "Tài khoản";
            dgvNhanVien.Columns[3].HeaderText = "Mật khẩu";
            dgvNhanVien.Columns[4].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[5].HeaderText = "Số ĐT";
            dgvNhanVien.Columns[6].HeaderText = "Email";
            dgvNhanVien.Columns[7].HeaderText = "Lương";
            List<NhanVien> nvs = nv.getKhachHang();
            dgvNhanVien.DataSource = nvs;
            txtDiaChi.Enabled = false;
            txtSoDT.Enabled = false;
            txtTenNV.Enabled = false;
            txtMaNV.Enabled = false;
            txtTaikhoan.Enabled = false;
            txtMK.Enabled= false;
            txtEmail.Enabled=false;
            txtLuong.Enabled=false;
            btnLuu.Enabled = false;
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
            int masv = int.Parse(row.Cells[0].Value.ToString());
            List<NhanVien> nvs = new List<NhanVien>();
            nvs = nv.getKhachHangTheoMa(masv);
            if (nvs.Count > 0)
            {
                NhanVien nhanvien = nvs[0];
                txtMaNV.Text = nhanvien.MaNhanVien.ToString();
                txtTenNV.Text = nhanvien.TenNhanVien;
                txtDiaChi.Text = nhanvien.DiaChi;
                txtSoDT.Text = nhanvien.SoDienThoai;
                txtTaikhoan.Text = nhanvien.user_name;
                txtMK.Text = nhanvien.pass;
                txtEmail.Text = nhanvien.Email;
                txtLuong.Text = nhanvien.Luong.ToString();
                decimal? totalSpending = nv.GetTotalSpendingByStaff(nhanvien.MaNhanVien);
                lb_tong.Text = (totalSpending ?? 0m).ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Them";
            txtMaNV.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDT.Enabled = true;
            txtTenNV.Enabled = true;
            txtTaikhoan.Enabled = true;
            txtMK.Enabled = true;
            txtEmail.Enabled = true;
            txtLuong.Enabled = true;
            btnLuu.Enabled = true;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Sua";
            //txtMaKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDT.Enabled = true;
            txtTenNV.Enabled = true;
            txtTaikhoan.Enabled = true;
            txtMK.Enabled = true;
            txtEmail.Enabled = true;
            txtLuong.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                QLNhanVien_GUI_Load(sender, e);
            }
            else
            {
                List<NhanVien> nvs = new List<NhanVien>();
                nvs = nv.getKhachHangTheoTen(searchText);
                dgvNhanVien.DataSource = nvs;
            }
        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm kiếm theo tên...")
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimkiem_Enter(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm kiếm theo tên...")
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lbCheck.Text == "Them")
            {
                NhanVien nv1 = new NhanVien();
                nv1.MaNhanVien = int.Parse(txtMaNV.Text);
                nv1.TenNhanVien = txtTenNV.Text.ToString().Trim();
                nv1.SoDienThoai = txtSoDT.Text.ToString().Trim();
                nv1.DiaChi= txtDiaChi.Text.ToString().Trim();
                nv1.user_name=txtTaikhoan.Text.Trim();
                nv1.pass=txtMK.Text.Trim();
                nv1.Email=txtEmail.Text.Trim();
                nv1.Luong = int.Parse(txtLuong.Text);
                if (string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtMaNV.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || 
                    string.IsNullOrEmpty(txtSoDT.Text)|| string.IsNullOrEmpty(txtTaikhoan.Text) || string.IsNullOrEmpty(txtMK.Text) || 
                    string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtLuong.Text))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    if (nv.KTMaNV(nv1.MaNhanVien) == true)
                    {
                        if (nv.KTSDT(nv1.SoDienThoai) == true)
                        {
                            try
                            {
                                
                                nv.ThemNV(nv1);
                                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                                QLNhanVien_GUI_Load(sender, e);
                                txtMaNV.Text = "";
                                txtSoDT.Text = "";
                                txtDiaChi.Text = "";
                                txtTenNV.Text = "";
                                txtTaikhoan.Text = "";
                                txtMK.Text = "";
                                txtEmail.Text = "";
                                txtLuong.Text = "";
                                lb_tong.Text = "";
                                button2.Enabled = true;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Thêm thất bại", "Thông báo", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Mã khách hàng đã tồn tại. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);
                    }

                }

            }
            if (lbCheck.Text == "Sua")
            {
                NhanVien nv1 = new NhanVien();
                nv1.MaNhanVien = int.Parse(txtMaNV.Text);
                nv1.TenNhanVien = txtTenNV.Text.ToString().Trim();
                nv1.SoDienThoai = txtSoDT.Text.ToString().Trim();
                nv1.DiaChi = txtDiaChi.Text.ToString().Trim();
                nv1.user_name = txtTaikhoan.Text.Trim();
                nv1.pass = txtMK.Text.Trim();
                nv1.Email = txtEmail.Text.Trim();
                nv1.Luong = int.Parse(txtLuong.Text);
                if (string.IsNullOrEmpty(txtTenNV.Text) || string.IsNullOrEmpty(txtMaNV.Text) || string.IsNullOrEmpty(txtDiaChi.Text) ||
                    string.IsNullOrEmpty(txtSoDT.Text) || string.IsNullOrEmpty(txtTaikhoan.Text) || string.IsNullOrEmpty(txtMK.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtLuong.Text))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        nv.SuaNV(nv1);
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                        QLNhanVien_GUI_Load(sender, e);
                        txtMaNV.Text = "";
                        txtSoDT.Text = "";
                        txtDiaChi.Text = "";
                        txtTenNV.Text = "";
                        txtTaikhoan.Text = "";
                        txtMK.Text = "";
                        txtEmail.Text = "";
                        txtLuong.Text = "";
                        lb_tong.Text = "";
                        btnThem.Enabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaNV.Text);
            if (nv.KTMaNV(ma) == true)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);

            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        nv.XoaNV(ma);
                        MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK);
                        QLNhanVien_GUI_Load(sender, e);
                        txtMaNV.Text = "";
                        txtSoDT.Text = "";
                        txtDiaChi.Text = "";
                        txtTenNV.Text = "";
                        txtTaikhoan.Text = "";
                        txtMK.Text = "";
                        txtEmail.Text = "";
                        txtLuong.Text = "";
                        lb_tong.Text = "";
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Xoá thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    return;
                }
                
            }
        }
    }
}
