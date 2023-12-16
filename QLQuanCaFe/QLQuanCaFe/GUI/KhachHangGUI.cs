using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL_BLL;

namespace QLQuanCaFe
{
    public partial class KhachHangGUI : Form
    {
        KhachHangDAL_BLL kh = new KhachHangDAL_BLL();
        HoaDonDAL_BLL hd = new HoaDonDAL_BLL();
        public KhachHang KhachHangDuocChon { get; private set; }
        public KhachHangGUI()
        {
            InitializeComponent();
            List<KhachHang> khs = kh.getKhachHang();
            dgvKhachhang.DataSource = khs;
            txtTimkiem.Text = "Tìm kiếm theo tên...";
            txtTimkiem.ForeColor = System.Drawing.Color.Gray;
        }

        private void dgvKhachhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             DataGridViewRow row = dgvKhachhang.Rows[e.RowIndex];
            int masv =  int.Parse(row.Cells[0].Value.ToString());
            List<KhachHang> khs = new List<KhachHang>();
            khs = kh.getKhachHangTheoMa(masv);
            if (khs.Count > 0)
            {
                KhachHang khachHang = khs[0];
                txtMaKH.Text=khachHang.MaKhachHang.ToString();
                txtTenKH.Text = khachHang.TenKhachHang;
                txtDiaChi.Text = khachHang.DiaChi;
                txtSoDT.Text = khachHang.SoDienThoai;
                decimal? totalSpending = kh.GetTotalSpendingByCustomer(khachHang.MaKhachHang);
                lb_chi.Text = (totalSpending ?? 0m).ToString();
            }
            
        }
        

        private void KhachHang_Load(object sender, EventArgs e)
        {
            dgvKhachhang.Columns[0].HeaderText = "Mã KH";
            dgvKhachhang.Columns[1].HeaderText = "Tên KH";
            dgvKhachhang.Columns[2].HeaderText = "Địa chỉ";
            dgvKhachhang.Columns[3].HeaderText = "Số ĐT";
            List<KhachHang> khs = kh.getKhachHang();
            dgvKhachhang.DataSource = khs;
            txtDiaChi.Enabled = false;
            txtSoDT.Enabled = false;
            txtTenKH.Enabled = false;
            txtMaKH.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Sua";
            //txtMaKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDT.Enabled = true;
            txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtTimkiem.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(searchText))
            {
                KhachHang_Load(sender, e);
            }
            else
            {
                List<KhachHang> khs = new List<KhachHang>();
                khs = kh.getKhachHangTheoTen(searchText);
                dgvKhachhang.DataSource = khs;
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

        private void btnThem_Click(object sender, EventArgs e)
        {
            lbCheck.Text = "Them";
            txtMaKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDT.Enabled = true;
            txtTenKH.Enabled = true;
            btnLuu.Enabled = true;
            button2.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (lbCheck.Text == "Them")
            {
                //int ma = int.Parse(txtMaKH.Text);
                string ten = txtTenKH.Text.ToString().Trim();
                string sodt = txtSoDT.Text.ToString().Trim();
                string diachi = txtDiaChi.Text.ToString().Trim();
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(diachi) || string.IsNullOrEmpty(sodt))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                   
                        if (kh.KTSDT(sodt) == true)
                        {
                            try
                            {
                                kh.ThemKH( ten, sodt, diachi);
                                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK);
                                KhachHang_Load(sender, e);
                                txtMaKH.Text = "";
                                txtSoDT.Text = "";
                                txtDiaChi.Text = "";
                                txtTenKH.Text = ""; button2.Enabled = true;
                                lb_chi.Text = "";
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

            }
            if (lbCheck.Text == "Sua")
            {
                int ma = int.Parse(txtMaKH.Text);
                string ten = txtTenKH.Text.ToString().Trim();
                string sodt = txtSoDT.Text.ToString().Trim();
                string diachi = txtDiaChi.Text.ToString().Trim();
                if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(txtMaKH.Text) || string.IsNullOrEmpty(diachi) || string.IsNullOrEmpty(sodt))
                {
                    MessageBox.Show("Không được để trống bất kì giá trị nào", "Thông báo", MessageBoxButtons.OK);
                }
                else
                {
                    try
                    {
                        kh.SuaKH(ma, ten, sodt, diachi);
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                        KhachHang_Load(sender, e);
                        txtMaKH.Text = "";
                        txtSoDT.Text = "";
                        txtDiaChi.Text = "";
                        txtTenKH.Text = "";
                        lb_chi.Text = "";
                        btnThem.Enabled = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }

            }
        }

        private void txtSoDT_Leave(object sender, EventArgs e)
        {
            String sdt = txtSoDT.Text;
            String reg = "^0\\d{9}$";
            if (!string.IsNullOrEmpty(sdt))
            {
                if (!Regex.IsMatch(sdt, reg))
                {
                    MessageBox.Show("Số điện thoại phải là 10 số và bắt đầu bằng số 0", "Thông báo", MessageBoxButtons.OK);
                    txtSoDT.Focus();
                }
            }
            else
            {

                MessageBox.Show("Không được để trống số đt", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaKH.Text);
            if (kh.KTMaKH(ma) == true)
            {
                MessageBox.Show("Mã khách hàng không hợp lệ. Vui lòng nhập lại!!", "Thông báo", MessageBoxButtons.OK);

            }
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Xác nhận xoá", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        hd.SuaHDtheoKH(ma);
                        kh.XoaKH(ma);
                        MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK);
                        KhachHang_Load(sender, e);
                        txtMaKH.Text = "";
                        txtSoDT.Text = "";
                        txtDiaChi.Text = "";
                        txtTenKH.Text = "";
                        lb_chi.Text = "";
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

        private void dgvKhachhang_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0 && rowIndex < dgvKhachhang.Rows.Count)
            {
                KhachHangDuocChon = (KhachHang)dgvKhachhang.Rows[rowIndex].DataBoundItem;
                this.Close(); // Đóng Form khi chọn xong
            }
        }
    }
}
